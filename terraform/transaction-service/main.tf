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

resource "aws_iam_role" "rds_ad_auth" {
  name                  = "ivas-transactions-db-ad-auth"
  description           = "Role used by RDS for Active Directory authentication and authorization"
  force_detach_policies = true
  assume_role_policy    = data.aws_iam_policy_document.rds_assume_role.json

  tags = local.default_tags
}

resource "aws_iam_role_policy_attachment" "rds_directory_services" {
  role       = aws_iam_role.rds_ad_auth.id
  policy_arn = "arn:aws:iam::aws:policy/service-role/AmazonRDSDirectoryServiceAccess"
}

################################################################################
# API Gateway Module
################################################################################

#resource "aws_route53_zone" "this" {
#  name = var.transactions_api_domain_name
#}
#
#module "acm" {
#  source  = "terraform-aws-modules/acm/aws"
#  version = "~> 3.0"
#
#  domain_name = var.transactions_api_domain_name
#  zone_id     = aws_route53_zone.this.zone_id
#
#  subject_alternative_names = [
#    "*.${var.transactions_api_domain_name}",
#    "app.dev.${var.transactions_api_domain_name}",
#  ] 
#
#  validate_certificate = true
#  wait_for_validation = true
#
#  tags = local.default_tags
#}

#module "api_gateway" {
#  source = "terraform-aws-modules/apigateway-v2/aws"
#
#  name          = local.transactions_api_gateway_name
#  description   = "IVAS Transactions Service API Gateway"
#  protocol_type = "HTTP"
#
#  cors_configuration = {
#    allow_headers = ["content-type", "x-amz-date", "authorization", "x-api-key", "x-amz-security-token", "x-amz-user-agent"]
#    allow_methods = ["*"]
#    allow_origins = ["*"]
#  }
#
#  default_route_settings = {
#    detailed_metrics_enabled = true
#    throttling_burst_limit   = 100
#    throttling_rate_limit    = 100
#  }
#  
#  # Routes and integrations
#  integrations = {
#    "POST /api/transactions" = {
#      lambda_arn             = module.transactions_lambda.lambda_function_arn
#      payload_format_version = "2.0"
#      timeout_milliseconds   = 12000
#    }
#    "GET /api/transactions" = {
#      lambda_arn             = module.transactions_lambda.lambda_function_arn
#      payload_format_version = "2.0"
#      timeout_milliseconds   = 12000
#    }
#    "$default" = {
#      lambda_arn = module.transactions_lambda.lambda_function_arn
#    }
#  }
#
#  tags = local.default_tags
#}

resource "aws_api_gateway_rest_api" "transactions_api" {

  name = "transactions_api"

  body = jsonencode({
    openapi = "3.0.1"
    info = {
      title   = "transactions_api"
      version = "1.0"
    }
    paths = {
      "/api/transactions" = {
        get = {
          x-amazon-apigateway-integration = {
            httpMethod           = "GET"
            payloadFormatVersion = "2.0"
            type                 = "HTTP_PROXY"
            uri                  = "https://ip-ranges.amazonaws.com/ip-ranges.json"
          }
        },
        post = {
          x-amazon-apigateway-integration = {
            httpMethod           = "POST"
            payloadFormatVersion = "2.0"
            type                 = "HTTP_PROXY"
            uri                  = "https://ip-ranges.amazonaws.com/ip-ranges.json"
          }
        },
        put = {
          x-amazon-apigateway-integration = {
            httpMethod           = "PUT"
            payloadFormatVersion = "2.0"
            type                 = "HTTP_PROXY"
            uri                  = "https://ip-ranges.amazonaws.com/ip-ranges.json"
          }
        },
        delete = {
          x-amazon-apigateway-integration = {
            httpMethod           = "DELETE"
            payloadFormatVersion = "2.0"
            type                 = "HTTP_PROXY"
            uri                  = "https://ip-ranges.amazonaws.com/ip-ranges.json"
          }
        },
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
  stage_name    = "transactions_api_stage"

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

  depends_on = [
    module.transactions_lambda_iam
  ]
}

################################################################################
# AWS Directory Service (Acitve Directory)
################################################################################

resource "aws_directory_service_directory" "ad_service_directory" {
  name     = "levitechnologies.ivas.com"
  password = "SuperSecretPassw0rd"
  edition  = "Standard"
  type     = "MicrosoftAD"

  vpc_settings {
    vpc_id = module.vpc.vpc_id
    # Only 2 subnets, must be in different AZs
    subnet_ids = slice(tolist(module.vpc.public_subnets), 0, 2)
  }

  tags = local.default_tags
}

################################################################################
# RDS Module
################################################################################

module "db" {
  source  = "terraform-aws-modules/rds/aws"
  version = "~> 3.0"

  identifier = local.transactions_db_name

  engine               = "sqlserver-ex"
  engine_version       = "14.00.3381.3.v1"
  family               = "sqlserver-ex-14.0" # DB parameter group
  major_engine_version = "14.00"             # DB option group
  instance_class       = "db.t3.small"

  allocated_storage     = 20
  max_allocated_storage = 40
  storage_encrypted     = false

  username               = "ivas"
  create_random_password = true
  random_password_length = 12
  port                   = 1433

  domain               = aws_directory_service_directory.ad_service_directory.id
  domain_iam_role_name = aws_iam_role.rds_ad_auth.name

  multi_az               = false
  subnet_ids             = module.vpc.private_subnets
  vpc_security_group_ids = [module.vpc.default_security_group_id]

  maintenance_window = "Mon:00:00-Mon:03:00"
  backup_window      = "03:00-06:00"

  backup_retention_period = 0
  skip_final_snapshot     = true
  deletion_protection     = false

  options                   = []
  create_db_parameter_group = false
  timezone                  = "GMT Standard Time"
  license_model             = "license-included"

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