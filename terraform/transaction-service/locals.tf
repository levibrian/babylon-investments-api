locals {
  transactions_lambda_name = "transactions"
  environment              = substr(terraform.workspace, 0, 3)
  logs_retention_in_days   = 14
  default_tags = {
    Stage   = local.environment
    Client  = var.client
    Service = var.service_name
    Version = var.version_tag
  }
}