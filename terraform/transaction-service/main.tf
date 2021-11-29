################################################################################
# IAM Role for Windows Authentication
################################################################################

module "transactions_lambda_iam" {
  source = "../modules/ivas-iam"

  create_lambda_role = true
  service_name       = var.service_name
  lambda_name        = local.transactions_lambda_name
  tags               = local.default_tags
}

################################################################################
# API Gateway Module
################################################################################

resource "aws_api_gateway_rest_api" "transactions_api" {

  name = local.transactions_api_gateway_name

  body = jsonencode({
    openapi = "3.0.1"
    info = {
      title   = "transactions_api"
      version = "1.0"
    }
    paths = {
      "/api/transactions" = {
        post = {
          x-amazon-apigateway-integration = {
            httpMethod           = "POST"
            payloadFormatVersion = "2.0"
            type                 = "HTTP_PROXY"
            uri                  = "https://ip-ranges.amazonaws.com/ip-ranges.json"
          }
        },
        #        put = {
        #          x-amazon-apigateway-integration = {
        #            httpMethod           = "PUT"
        #            payloadFormatVersion = "2.0"
        #            type                 = "HTTP_PROXY"
        #            uri                  = "https://ip-ranges.amazonaws.com/ip-ranges.json"
        #          }
        #        },
        delete = {
          x-amazon-apigateway-integration = {
            httpMethod           = "DELETE"
            payloadFormatVersion = "2.0"
            type                 = "HTTP_PROXY"
            uri                  = "https://ip-ranges.amazonaws.com/ip-ranges.json"
          }
        },
      },
      "/api/portfolio" = {
        get = {
          x-amazon-apigateway-integration = {
            httpMethod           = "GET"
            payloadFormatVersion = "2.0"
            type                 = "HTTP_PROXY"
            uri                  = "https://ip-ranges.amazonaws.com/ip-ranges.json"
          }
        }
      }
    }
  })

  tags = local.default_tags
}

resource "aws_api_gateway_deployment" "transactions_api_deployment" {
  rest_api_id = aws_api_gateway_rest_api.transactions_api.id

  triggers = {
    redeployment = sha1(jsonencode(aws_api_gateway_rest_api.transactions_api.body))
  }

  lifecycle {
    create_before_destroy = true
  }
}

resource "aws_api_gateway_stage" "transactions_api_stage" {
  deployment_id = aws_api_gateway_deployment.transactions_api_deployment.id
  rest_api_id   = aws_api_gateway_rest_api.transactions_api.id
  stage_name    = "${local.transactions_api_gateway_name}-stage"

  tags = local.default_tags
}

resource "aws_api_gateway_method_settings" "transactions_api_settings" {
  rest_api_id = aws_api_gateway_rest_api.transactions_api.id
  stage_name  = aws_api_gateway_stage.transactions_api_stage.stage_name
  method_path = "*/*"

  settings {
    metrics_enabled    = false
    data_trace_enabled = false
  }
}

################################################################################
# Lambda Module
################################################################################

module "transactions_lambda" {
  source  = "terraform-aws-modules/lambda/aws"
  version = "2.23.0"

  function_name = local.transactions_lambda_name
  description   = "Lambda to store and analyze an investment portfolio."
  handler       = "Ivas.Transactions.Api::Ivas.Transactions.Api.LambdaEntryPoint::FunctionHandlerAsync"
  runtime       = "dotnetcore3.1"
  memory_size   = 256

  create_package         = false
  local_existing_package = local.transactions_lambda_file_path

  vpc_subnet_ids         = module.vpc.private_subnets
  vpc_security_group_ids = [module.vpc.default_security_group_id]
  attach_network_policy  = true

  tags = local.default_tags

  environment_variables = {
    TRANSACTIONS_DYNAMO_DB_TABLE = aws_dynamodb_table.transactions_dynamodb_table.name
  }

  depends_on = [
    module.transactions_lambda_iam
  ]
}

################################################################################
# Dynamo DB
################################################################################

resource "aws_dynamodb_table" "transactions_dynamodb_table" {
  name           = local.transactions_dynamodb_table_name
  billing_mode   = "PROVISIONED"
  read_capacity  = 20
  write_capacity = 20
  hash_key       = "UserId"
  range_key      = "TransactionId"

  attribute {
    name = "UserId"
    type = "N"
  }

  attribute {
    name = "TransactionId"
    type = "S"
  }

  tags = local.default_tags
}

################################################################################
# Supporting Resources
################################################################################

module "vpc" {
  source  = "terraform-aws-modules/vpc/aws"
  version = "~> 2"

  name = local.transactions_resource_base_name
  cidr = "10.99.0.0/18"

  azs             = ["${var.region}a", "${var.region}b"]
  public_subnets  = ["10.99.0.0/24", "10.99.1.0/24"]
  private_subnets = ["10.99.3.0/24", "10.99.4.0/24"]

  create_database_subnet_group = false

  tags = local.default_tags
}

module "security_group" {
  source  = "terraform-aws-modules/security-group/aws"
  version = "~> 4"

  name        = "${local.transactions_resource_base_name}-db-security-group"
  description = "Complete SqlServer example security group"
  vpc_id      = module.vpc.vpc_id

  # ingress
  ingress_with_cidr_blocks = [
    {
      from_port   = 1433
      to_port     = 1433
      protocol    = "tcp"
      description = "SqlServer access from within VPC"
      cidr_blocks = module.vpc.vpc_cidr_block
    },
  ]

  tags = local.default_tags
}

################################################################################
# Application Manager
################################################################################

resource "aws_resourcegroups_group" "main" {
  name = "${local.transactions_resource_base_name}-resource-group"

  resource_query {
    query = <<JSON
    {
      "ResourceTypeFilters": ["AWS::AllSupported"],
      "TagFilters": [
        {
          "Key": "Service",
          "Values": ["${var.service_name}"]
        },
        {
          "Key": "Stage",
          "Values": ["${local.environment}"]
        },
        {
          "Key": "ServiceGroup",
          "Values": ["${local.transactions_resource_base_name}"]
        }
      ]
    }
    JSON
  }

  tags = merge(local.default_tags, tomap({
    "Name"  = local.transactions_resource_base_name,
    "Stage" = terraform.workspace
  }))
}