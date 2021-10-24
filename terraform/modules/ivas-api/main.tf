resource "aws_api_gateway_rest_api" "rest_api" {
  name        = "${var.name_prefix}-${var.api_name}"
  description = var.description

  endpoint_configuration {
    types            = [var.endpoint_configuration_type]
    vpc_endpoint_ids = var.vpc_endpoint_ids
  }
  policy = var.policy
  tags = merge(var.tags, tomap({
    "Name"       = "${var.name_prefix}-${var.api_name}",
    "ApiLabel"   = var.api_label,
    "LambdaName" = var.function_name
  }))
}

resource "aws_api_gateway_stage" "rest_api_stage" {
  rest_api_id          = aws_api_gateway_rest_api.rest_api.id
  stage_name           = var.environment
  deployment_id        = aws_api_gateway_deployment.rest_api_deployment.id
  xray_tracing_enabled = false

  access_log_settings {
    destination_arn = aws_cloudwatch_log_group.rest_api_log_group.arn
    format          = jsonencode(local.access_log_settings)
  }

  tags = var.tags
}

resource "aws_cloudwatch_log_group" "rest_api_log_group" {
  name              = "/aws/apigateway/${var.name_prefix}-${var.api_name}-${var.api_label}/access"
  retention_in_days = var.logs_retention_in_days
  tags              = var.tags
}

resource "aws_api_gateway_deployment" "rest_api_deployment" {
  rest_api_id = aws_api_gateway_rest_api.rest_api.id
  description = "${var.service_name} ${var.api_label} Rest Api Deployment"
  triggers = {
    redeployment = timestamp()
  }
  lifecycle {
    create_before_destroy = true
  }
  depends_on = [aws_api_gateway_method.rest_api_method, aws_api_gateway_integration.rest_api_integration, aws_cloudwatch_log_group.rest_api_log_group]
}


resource "aws_api_gateway_resource" "rest_api_resource" {
  rest_api_id = aws_api_gateway_rest_api.rest_api.id
  parent_id   = aws_api_gateway_rest_api.rest_api.root_resource_id
  path_part   = "{proxy+}"
}

resource "aws_api_gateway_method" "rest_api_method" {
  rest_api_id          = aws_api_gateway_rest_api.rest_api.id
  resource_id          = aws_api_gateway_resource.rest_api_resource.id
  http_method          = "ANY"
  authorization        = var.authorizer
  authorizer_id        = var.authorizer_id
  authorization_scopes = var.authorization_scopes
  request_parameters = {
    "method.request.path.proxy" = true
  }
}

resource "aws_api_gateway_method_settings" "rest_api_method_settings" {
  rest_api_id = aws_api_gateway_rest_api.rest_api.id
  stage_name  = aws_api_gateway_stage.rest_api_stage.stage_name
  method_path = "*/*"

  settings {
    logging_level      = "INFO"
    data_trace_enabled = false
  }
}

resource "aws_api_gateway_integration" "rest_api_integration" {
  rest_api_id             = aws_api_gateway_rest_api.rest_api.id
  resource_id             = aws_api_gateway_resource.rest_api_resource.id
  http_method             = aws_api_gateway_method.rest_api_method.http_method
  integration_http_method = "POST"
  type                    = "AWS_PROXY"
  uri                     = data.aws_lambda_function.invoke_lambda.invoke_arn
}

resource "aws_lambda_permission" "allow_api_gateway" {
  function_name = var.lambda_arn
  statement_id  = "AllowExecutionFromApiGateway"
  action        = "lambda:InvokeFunction"
  principal     = "apigateway.amazonaws.com"
  source_arn    = "arn:aws:execute-api:${local.region}:${var.account_id}:${aws_api_gateway_rest_api.rest_api.id}/*/*"
}
