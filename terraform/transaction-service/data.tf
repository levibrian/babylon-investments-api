################################################################################
# IAM
################################################################################

data "aws_iam_policy_document" "rds_assume_role" {
  statement {
    sid = "AssumeRole"

    actions = [
      "sts:AssumeRole",
    ]

    principals {
      type        = "Service"
      identifiers = ["rds.amazonaws.com"]
    }
  }
}

################################################################################
# Route 53
################################################################################

#data "aws_route53_zone" "this" {
#  name         = var.transactions_api_domain_name
#  private_zone = false
#}