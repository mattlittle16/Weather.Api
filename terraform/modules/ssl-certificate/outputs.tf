output "certificate_name" {
  description = "Name of the Lightsail certificate"
  value       = aws_lightsail_certificate.weather_api.name
}

output "certificate_arn" {
  description = "ARN of the Lightsail certificate"
  value       = aws_lightsail_certificate.weather_api.arn
}

output "certificate_domain_name" {
  description = "Domain name of the certificate"
  value       = aws_lightsail_certificate.weather_api.domain_name
}

output "domain_validation_options" {
  description = "Domain validation options for DNS validation"
  value       = aws_lightsail_certificate.weather_api.domain_validation_options
}