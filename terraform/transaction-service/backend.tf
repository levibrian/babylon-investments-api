terraform {
  backend "s3" {
    bucket  = "babylon-terraform-states"
    region  = "eu-west-1"
    key     = "investments-infra.tfstate"
    encrypt = "true"
  }
}