provider "aws" {
  region = "eu-west-1"
}

provider "aws" {
  alias                   = "default"
  region                  = "eu-west-1"
  allowed_account_ids     = [var.account_ids_default]
  shared_credentials_file = "~/.aws/credentials"
  profile                 = "default"
}