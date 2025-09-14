terraform {
  required_version = ">= 1.0"
  
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }

  # Uncomment and configure for remote state
  # backend "s3" {
  #   bucket = "your-terraform-state-bucket"
  #   key    = "weather-api/terraform.tfstate"
  #   region = "us-east-1"
  # }
}

provider "aws" {
  region = var.aws_region
}

# ECR Repository
module "ecr" {
  source = "./modules/ecr"

  repository_name = var.repository_name
  tags           = var.tags
}

# SSL Certificate
module "ssl_certificate" {
  source = "./modules/ssl-certificate"

  certificate_name = var.certificate_name
  domain_name     = var.domain_name
  hosted_zone_id  = var.hosted_zone_id

  tags = var.tags
}

# Lightsail Container Service
module "lightsail_container" {
  source = "./modules/lightsail-container"

  service_name         = var.service_name
  container_power     = var.container_power
  container_scale     = var.container_scale
  container_image     = "${module.ecr.repository_url}:${var.image_tag}"
  environment_name    = var.environment_name
  
  # Sensitive variables
  openweather_api_key = var.openweather_api_key
  api_key            = var.api_key
  
  # API configuration
  openweather_base_url      = var.openweather_base_url
  rate_limit               = var.rate_limit
  rate_limit_time_minutes  = var.rate_limit_time_minutes
  daily_api_limit         = var.daily_api_limit

  # SSL certificate (will be attached after validation)
  certificate_name      = module.ssl_certificate.certificate_name
  domain_names         = [var.domain_name]
  attach_custom_domain = var.attach_custom_domain

  # ECR repository ARN for private registry access
  ecr_repository_arn = module.ecr.repository_arn

  tags = var.tags

  depends_on = [module.ssl_certificate]
}

# DNS Configuration - Points subdomain to Lightsail container service
module "dns" {
  source = "./modules/dns"

  hosted_zone_id           = var.hosted_zone_id
  subdomain               = "weather-api"
  domain_name             = var.domain_name
  lightsail_service_name  = module.lightsail_container.service_name
  lightsail_default_domain = module.lightsail_container.service_url
  ttl                     = var.dns_ttl

  tags = var.tags

  depends_on = [module.lightsail_container]
}