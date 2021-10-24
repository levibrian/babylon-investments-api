variable "name_prefix" {}

variable "api_name" {}
variable "description" {}
variable "environment" {}
variable "service_name" {}
variable "function_name" {}
variable "lambda_arn" {}
variable "account_id" {}

variable "logs_retention_in_days" {
  type    = number
  default = 400
}

variable "tags" { type = map(any) }

variable "api_label" {
  type    = string
  default = "internal"
}

variable "endpoint_configuration_type" { type = string }

variable "vpc_endpoint_ids" {
  type = list(string)

}

variable "policy" {
  type    = string
  default = ""
}

variable "authorizer" {
  type    = string
  default = "NONE"
}

variable "authorizer_id" {
  type    = string
  default = ""
}

variable "authorization_scopes" {
  type    = list(string)
  default = null
}

variable "set_dns_alias" { default = false }

variable "api_domain_name" {
  type    = string
  default = ""
}

variable "api_certificate_arn" {
  type    = string
  default = ""
}