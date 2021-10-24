variable "name_prefix" { default = "" }
variable "service_name" {}

variable "tags" { type = map(any) }

variable "lambda_name" { default = "" }
variable "purpose" { default = "" }
variable "allow_assume_role_lambda" { default = "" }

variable "create_lambda_role" { default = false }

variable "policy" { default = "" }
