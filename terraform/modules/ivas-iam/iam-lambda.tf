resource "aws_iam_role" "lambda_role" {
  count              = var.create_lambda_role ? 1 : 0
  name               = "${var.lambda_name}-iam"
  assume_role_policy = <<EOF
{
    "Version": "2012-10-17",
    "Statement": [
      {
        "Action": "sts:AssumeRole",
        "Principal": {
          "Service": "lambda.amazonaws.com"
        },
        "Effect": "Allow",
        "Sid": ""
      }
    ]
}
  EOF
  tags               = var.tags
}

resource "aws_iam_role_policy" "lambda_policy" {
  count  = var.create_lambda_role ? 1 : 0
  name   = "${var.lambda_name}-policy"
  role   = aws_iam_role.lambda_role[0].name
  policy = <<EOF
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Action": [
        "logs:CreateLogGroup",
        "logs:CreateLogStream",
        "logs:PutLogEvents"
      ],
      "Resource": "arn:aws:logs:*:*:*",
      "Effect": "Allow"
    },
    {
      "Effect": "Allow",
      "Action": [
        "dynamodb:*" 
      ],
      "Resource": "*"
    },
    {
      "Effect": "Allow",
      "Action":[
        "iam:GetPolicy",
        "iam:GetPolicyVersion",
        "iam:GetRole",
        "iam:GetRolePolicy",
        "iam:ListAttachedRolePolicies",
        "iam:ListRolePolicies",
        "iam:ListRoles",
        "iam:PassRole" 
      ],
      "Resource": "*"
    }
  ]
}
EOF
}

