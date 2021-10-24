module "lambda" {
  source  = "terraform-module/lambda/aws"
  version = "2.10.0"

  function_name    = var.config.function_name
  filename         = "${var.path}/${var.config.filename}"
  description      = var.config.description
  handler          = var.config.handler
  runtime          = var.runtime
  memory_size      = var.config.memory_size
  concurrency      = var.config.concurrency
  lambda_timeout   = var.lambda_timeout
  log_retention    = var.log_retention
  role_arn         = var.config.role_arn
  tracing_config   = var.tracing_config
  source_code_hash = filebase64sha256("${var.path}/${var.config.filename}")

  vpc_config = {
    subnet_ids         = [data.aws_ssm_parameter.subnet_id_1.value, data.aws_ssm_parameter.subnet_id_2.value]
    security_group_ids = [var.security_group_id]
  }

  environment = var.config.environment_variables

  tags = merge(var.tags, tomap({
    "Name"      = var.config.function_name,
    "Workspace" = terraform.workspace
  }))
}