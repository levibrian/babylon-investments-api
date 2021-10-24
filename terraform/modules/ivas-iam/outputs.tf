output "lambda_role_arn" {
  description = "ARN of IAM role for a lambda"
  value       = element(concat(aws_iam_role.lambda_role.*.arn, [""]), 0)
}