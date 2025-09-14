output "repository_url" {
  description = "URL of the ECR repository"
  value       = aws_ecr_repository.weather_api.repository_url
}

output "repository_arn" {
  description = "ARN of the ECR repository"
  value       = aws_ecr_repository.weather_api.arn
}

output "repository_name" {
  description = "Name of the ECR repository"
  value       = aws_ecr_repository.weather_api.name
}