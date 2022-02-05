locals {
  Investments_resource_base_name  = "${var.client}-${local.environment}-Investments"
  Investments_api_gateway_name    = "${local.Investments_resource_base_name}-api"
  Investments_lambda_name         = "${local.Investments_resource_base_name}-lambda"
  Investments_dynamodb_table_name = "${local.Investments_resource_base_name}-table"
  Investments_lambda_file_path    = "${var.packages_path}/${var.package_file_name}"
  Investments_subdomain_name      = "${local.Investments_resource_base_name}-api-subdomain-name"
  environment                      = var.env_suffix == "" ? substr(terraform.workspace, 0, 3) : var.env_suffix
  logs_retention_in_days           = 14
  default_tags = {
    Stage        = local.environment
    Client       = var.client
    Service      = var.service_name
    ServiceGroup = local.Investments_resource_base_name
    Version      = var.version_tag
  }
}