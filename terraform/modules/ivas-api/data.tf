data "aws_lambda_function" "invoke_lambda" {
  function_name = var.function_name
}
data "aws_region" "current" {}