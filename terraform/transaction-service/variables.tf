variable "client" {}
variable "account_id" { type = map(any) }
variable "region" {}

variable "service_name" {}
variable "version_tag" { default = "" }

variable "account_ids_default" { default = "480481321925" }

variable "packages_path" {
  type    = string
  default = "../../artifacts"
}

variable "transactions_lambda_packaged_file_name" {
  type    = string
  default = "Ivas.Transactions.Api.zip"
}