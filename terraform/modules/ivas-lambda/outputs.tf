output "arn" {
  value       = module.lambda.arn
  description = "Lambda ARN"
}

output "name" {
  value       = module.lambda.name
  description = "Lambda name"
}