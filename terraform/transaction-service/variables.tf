variable "account_id" {
  type = map(any)
}

variable "region" {
  type    = string
  default = "eu-west-1"
}

variable "version_tag" {
  type    = string
  default = "1.0.0"
}

variable "client" {
  type    = string
  default = "ivas"
}

variable "service_name" {
  type    = string
  default = "ivas-transactions-service"
}

variable "account_ids_default" { default = "480481321925" }

variable "packages_path" {
  type    = string
  default = "../../artifacts"
}

variable "package_file_name" {
  type    = string
  default = "Ivas.Transactions.Api.zip"
}

variable "transactions_api_domain_name" {
  type    = string
  default = "ivas.transactions.levitechnologies.com"
}