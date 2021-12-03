terraform {
  backend "s3" {
    bucket  = "ivas-terraform-states"
    region  = "eu-west-1"
    key     = "transactions-infra.tfstate"
    encrypt = "true"
  }
}