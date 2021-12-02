terraform {
  required_version = "~>0.15"

  backend "s3" {
    bucket  = "ivas-terraform-states"
    region  = "eu-west-1"
    key     = "transactions-infra.tfstate"
    encrypt = "true"
  }
}