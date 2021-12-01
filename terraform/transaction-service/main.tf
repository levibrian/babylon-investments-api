################################################################################
# IAM Role
################################################################################

resource "aws_iam_role" "lambda_role" {
  count              = 1
  name               = "${local.transactions_lambda_name}-iam"
  assume_role_policy = <<EOF
{
    "Version": "2012-10-17",
    "Statement": [
      {
        "Action": "sts:AssumeRole",
        "Principal": {
          "Service": "lambda.amazonaws.com"
        },
        "Effect": "Allow",
        "Sid": ""
      }
    ]
}
  EOF
  tags               = local.default_tags
}

resource "aws_iam_role_policy" "lambda_policy" {
  count  = 1
  name   = "${local.transactions_lambda_name}-policy"
  role   = aws_iam_role.lambda_role[0].name
  policy = <<EOF
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Action": [
        "logs:CreateLogGroup",
        "logs:CreateLogStream",
        "logs:PutLogEvents"
      ],
      "Resource": "arn:aws:logs:*:*:*",
      "Effect": "Allow"
    },
    {
      "Effect": "Allow",
      "Action": [
        "dynamodb:*" 
      ],
      "Resource": "*"
    },
    {
      "Effect": "Allow",
      "Action": [
        "ec2:*" 
      ],
      "Resource": "*"
    },
    {
      "Effect": "Allow",
      "Action":[
        "iam:GetPolicy",
        "iam:GetPolicyVersion",
        "iam:GetRole",
        "iam:GetRolePolicy",
        "iam:ListAttachedRolePolicies",
        "iam:ListRolePolicies",
        "iam:ListRoles",
        "iam:PassRole" 
      ],
      "Resource": "*"
    }
  ]
}
EOF
}

resource "aws_lambda_permission" "lambda_apigateway_trigger" {
  statement_id  = "AllowAPIGatewayInvoke"
  action        = "lambda:InvokeFunction"
  function_name = module.transactions_lambda.lambda_function_arn
  principal     = "apigateway.amazonaws.com"

  #--------------------------------------------------------------------------------
  # Per deployment
  #--------------------------------------------------------------------------------
  # The /*/*  grants access from any method on any resource within the deployment.
  source_arn = "${module.api_gateway.apigatewayv2_api_execution_arn}/*/*"

  #--------------------------------------------------------------------------------
  # Per API
  #--------------------------------------------------------------------------------
  # The /*/*/* part allows invocation from any stage, method and resource path
  # within API Gateway REST API.
  #source_arn    = "${module.api_gateway.apigatewayv2_api_execution_arn}/*/*/*"
}

################################################################################
# API Gateway Module
################################################################################

module "api_gateway" {
  source = "terraform-aws-modules/apigateway-v2/aws"

  name          = local.transactions_api_gateway_name
  description   = "IVAS Api Gateway."
  protocol_type = "HTTP"

  cors_configuration = {
    allow_headers = ["content-type", "x-amz-date", "authorization", "x-api-key", "x-amz-security-token", "x-amz-user-agent"]
    allow_methods = ["*"]
    allow_origins = ["*"]
  }

  create_api_domain_name = false # to control creation of API Gateway Domain Name

  default_route_settings = {
    detailed_metrics_enabled = false
    throttling_burst_limit   = 100
    throttling_rate_limit    = 100
  }

  create_routes_and_integrations = true

  integrations = {
    
    "GET /ivas/api/transactions" = {
      lambda_arn             = "arn:aws:apigateway:${var.region}:lambda:path/2015-03-31/functions/${module.transactions_lambda.lambda_function_arn}/invocations"
      integration_type       = "AWS_PROXY"
      payload_format_version = "2.0"
      authorization_type     = "NONE"
    }
    "POST /ivas/api/transactions" = {
      lambda_arn             = "arn:aws:apigateway:${var.region}:lambda:path/2015-03-31/functions/${module.transactions_lambda.lambda_function_arn}/invocations"
      integration_type       = "AWS_PROXY"
      payload_format_version = "2.0"
      authorization_type     = "NONE"
      timeout_milliseconds   = 12000
    }
    "DELETE /ivas/api/transactions" = {
      lambda_arn             = "arn:aws:apigateway:${var.region}:lambda:path/2015-03-31/functions/${module.transactions_lambda.lambda_function_arn}/invocations"
      integration_type       = "AWS_PROXY"
      payload_format_version = "2.0"
      authorization_type     = "NONE"
      timeout_milliseconds   = 12000
    }
    "GET /ivas/api/portfolios" = {
      lambda_arn             = "arn:aws:apigateway:${var.region}:lambda:path/2015-03-31/functions/${module.transactions_lambda.lambda_function_arn}/invocations"
      integration_type       = "AWS_PROXY"
      payload_format_version = "2.0"
      authorization_type     = "NONE"
      timeout_milliseconds   = 12000
    }
  }

  vpc_links = {
    ivas-dev-vpc = {
      name               = "${local.transactions_resource_base_name}-api-gateway-vpc-links"
      security_group_ids = [module.api_gateway_security_group.security_group_id]
      subnet_ids         = module.vpc.public_subnets
    }
  }

  default_stage_tags = local.default_tags
  vpc_link_tags      = local.default_tags
  tags               = local.default_tags
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
  vpc_security_group_ids = [module.lambda_security_group.security_group_id]

  attach_policy_json = true
  policy_json        = aws_iam_role_policy.lambda_policy[0].policy

  lambda_role = aws_iam_role.lambda_role[0].arn

  tags = local.default_tags

  environment_variables = {
    TRANSACTIONS_DYNAMO_DB_TABLE = aws_dynamodb_table.transactions_dynamodb_table.name
  }
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
  cidr = "10.79.0.0/16"

  azs             = ["${var.region}a", "${var.region}b"]
  public_subnets  = ["10.79.1.0/24", "10.79.2.0/24"]
  private_subnets = ["10.79.3.0/24", "10.79.4.0/24"]

  create_database_subnet_group = false

  tags = local.default_tags
}

module "api_gateway_security_group" {
  source  = "terraform-aws-modules/security-group/aws"
  version = "~> 4.0"

  name        = "${local.transactions_resource_base_name}-apg-security-group"
  description = "Security group for exposing the IVAS Api Gateway."
  vpc_id      = module.vpc.vpc_id

  ingress_cidr_blocks = ["0.0.0.0/0"]
  ingress_rules       = ["http-80-tcp"]

  egress_rules = ["all-all"]
}

module "lambda_security_group" {
  source  = "terraform-aws-modules/security-group/aws"
  version = "~> 4.0"

  name        = "${local.transactions_resource_base_name}-lambda-security-group"
  description = "Lambda security group for IVAS Transactions Lambda"
  vpc_id      = module.vpc.vpc_id

  computed_ingress_with_source_security_group_id = [
    {
      rule                     = "http-80-tcp"
      source_security_group_id = module.api_gateway_security_group.security_group_id
    }
  ]

  number_of_computed_ingress_with_source_security_group_id = 1

  egress_rules = ["all-all"]
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