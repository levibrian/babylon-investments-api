################################################################################
# VPC Endpoint
################################################################################

# Data source used to avoid race condition
data "aws_vpc_endpoint_service" "dynamodb" {
  service = "dynamodb"

  filter {
    name   = "service-type"
    values = ["Gateway"]
  }
}

data "aws_iam_policy_document" "dynamodb_endpoint_policy" {
  statement {
    effect    = "Allow"
    actions   = ["dynamodb:*"]
    resources = ["*"]

    principals {
      type        = "*"
      identifiers = ["*"]
    }

    condition {
      test     = "StringNotEquals"
      variable = "aws:sourceVpce"

      values = [data.aws_vpc_endpoint_service.dynamodb.id]
    }
  }
}