# AWS Configuration
variable "aws_region" {
  description = "AWS region for resources"
  type        = string
  default     = "us-east-1"
}

# Domain Configuration
variable "domain_name" {
  description = "The domain name for the Weather API"
  type        = string
  default     = "weather-api.mattlittle.me"
}

variable "hosted_zone_id" {
  description = "Route53 hosted zone ID for mattlittle.me"
  type        = string
  default     = "Z32TZDXWWXB3BW"
}

variable "dns_ttl" {
  description = "TTL for DNS records"
  type        = number
  default     = 300
}

# Lightsail Configuration
variable "service_name" {
  description = "Name for the Lightsail container service"
  type        = string
  default     = "weather-api"
}

variable "container_power" {
  description = "Power specification for the container (nano, micro, small, medium, large)"
  type        = string
  default     = "micro"
}

variable "container_scale" {
  description = "Number of container instances to run"
  type        = number
  default     = 1
}

# ECR Configuration
variable "repository_name" {
  description = "Name of the ECR repository"
  type        = string
  default     = "weather-api"
}

variable "image_tag" {
  description = "Docker image tag to deploy"
  type        = string
  default     = "latest"
}

# Legacy variable - keeping for compatibility but using ECR module instead
variable "container_image" {
  description = "Docker image for the container (deprecated - using ECR module now)"
  type        = string
  default     = ""
}

variable "environment_name" {
  description = "Environment name (e.g., production, staging)"
  type        = string
  default     = "production"
}

# SSL Certificate
variable "certificate_name" {
  description = "Name for the SSL certificate"
  type        = string
  default     = "weather-api-cert"
}

# API Configuration
variable "openweather_api_key" {
  description = "OpenWeather API key"
  type        = string
  sensitive   = true
}

variable "api_key" {
  description = "API key for accessing the Weather API"
  type        = string
  sensitive   = true
}

variable "openweather_base_url" {
  description = "Base URL for OpenWeather API"
  type        = string
  default     = "https://api.openweathermap.org"
}

variable "rate_limit" {
  description = "Rate limit per time window"
  type        = number
  default     = 100
}

variable "rate_limit_time_minutes" {
  description = "Rate limit time window in minutes"
  type        = number
  default     = 60
}

variable "daily_api_limit" {
  description = "Daily API request limit"
  type        = number
  default     = 1000
}

# SSL Certificate Attachment
variable "attach_custom_domain" {
  description = "Whether to attach custom domain (set to true after certificate validation)"
  type        = bool
  default     = false
}

# Tags
variable "tags" {
  description = "Common tags for all resources"
  type        = map(string)
  default = {
    Project     = "weather-api"
    Environment = "production"
    ManagedBy   = "terraform"
  }
}