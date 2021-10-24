module "transactions_lambda_iam" {
  source = "../modules/ivas-iam"

  create_lambda_role = true
  service_name       = var.service_name
  lambda_name        = local.transactions_lambda_name
  tags               = local.default_tags
}

module "transactions_lambda" {
  source            = "../modules/ivas-lambda"
  path              = var.packages_path
  environment       = local.environment
  tags              = local.default_tags
  security_group_id = aws_security_group.security_group.id
  log_retention     = local.logs_retention_in_days
  config = {
    description    = "Lambda to CRUD Transactions. Furthermore, the main purpose of this API is to store and analyze an investment portfolio."
    function_name  = "${var.service_name}-${local.transactions_lambda_name}"
    handler       = "Ivas.Transactions.Api::Ivas.Transactions.Api.LambdaEntryPoint::FunctionHandlerAsync"
    filename      = "Ivas.Transactions.Api.zip"
    role_arn       = module.transactions_lambda_iam.lambda_role_arn
    memory_size    = 256
    concurrency    = -1
    lambda_timeout = 30
  }

  depends_on = [
    module.transactions_lambda_iam
  ]
}

resource "aws_security_group" "security_group" {
  description = "Allow access to internet"
  vpc_id      = aws_vpc.main.id

  ingress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }

  tags = local.default_tags
}

resource "aws_vpc" "main" {
  cidr_block       = "10.0.0.0/16"
  instance_tenancy = "default"

  tags = {
    Name = "main"
  }
}