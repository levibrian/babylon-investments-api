provider "aws" {
  alias                   = "default"
  region                  = var.region
  allowed_account_ids     = [var.account_ids_default]
  shared_credentials_file = "~/.aws/credentials"
  profile                 = "default"
}