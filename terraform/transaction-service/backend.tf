terraform {
  backend "s3" {
    bucket  = "babylon-terraform-states"
    region  = "eu-west-1"
    key     = "transactions-infra.tfstate"
    encrypt = "true"
  }
}