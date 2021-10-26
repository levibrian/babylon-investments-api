module "transactions_lambda_iam" {
  source = "../modules/ivas-iam"

  create_lambda_role = true
  service_name       = var.service_name
  lambda_name        = local.transactions_lambda_name
  tags               = local.default_tags
}

module "transactions_lambda" {
  source  = "terraform-module/lambda/aws"
  version = "2.10.0"

  function_name    = "${var.service_name}-${local.transactions_lambda_name}"
  filename         = var.transactions_lambda_packaged_file_name
  description      = "Lambda to store and analyze an investment portfolio."
  handler          = "Ivas.Transactions.Api::Ivas.Transactions.Api.LambdaEntryPoint::FunctionHandlerAsync"
  runtime          = "dotnetcore3.1"
  memory_size      = 256
  concurrency      = -1
  lambda_timeout   = 30
  log_retention    = 400
  role_arn         = module.transactions_lambda_iam.lambda_role_arn
  source_code_hash = filebase64sha256("${var.packages_path}/${var.transactions_lambda_packaged_file_name}")

  vpc_config = {
    subnet_ids         = [aws_subnet.main.id]
    security_group_ids = [aws_security_group.security_group.id]
  }

  tags = merge(tomap({
    "Name"      = "${var.service_name}-${local.transactions_lambda_name}",
    "Workspace" = terraform.workspace
  }))

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

resource "aws_subnet" "main" {
  vpc_id     = aws_vpc.main.id
  cidr_block = "10.0.1.0/24"

  tags = {
    Name = "Main"
  }
}

resource "aws_vpc" "main" {
  cidr_block       = "10.0.0.0/16"
  instance_tenancy = "default"

  tags = {
    Name = "main"
  }
}