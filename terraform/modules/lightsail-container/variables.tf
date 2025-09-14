variable "service_name" {
  description = "Name of the Lightsail container service"
  type        = string
  default     = "weather-api"
}

variable "container_power" {
  description = "Power specification for the container service (nano, micro, small, medium, large, xlarge)"
  type        = string
  default     = "micro"
}

variable "container_scale" {
  description = "Scale specification for the container service"
  type        = number
  default     = 1
}

variable "container_image" {
  description = "Docker image for the container"
  type        = string
  default     = "weather-api:latest"
}

variable "environment_name" {
  description = "Environment name (e.g., prod, dev)"
  type        = string
  default     = "Production"
}

variable "openweather_api_key" {
  description = "OpenWeather API key"
  type        = string
  sensitive   = true
}

variable "api_key" {
  description = "Weather API authentication key"
  type        = string
  sensitive   = true
}

variable "openweather_base_url" {
  description = "OpenWeather API base URL"
  type        = string
  default     = "https://api.openweathermap.org"
}

variable "rate_limit" {
  description = "API rate limit per minute"
  type        = number
  default     = 50
}

variable "rate_limit_time_minutes" {
  description = "Rate limit time window in minutes"
  type        = number
  default     = 1
}

variable "daily_api_limit" {
  description = "Daily OpenWeather API limit"
  type        = number
  default     = 990
}

variable "certificate_name" {
  description = "Name of the SSL certificate to attach"
  type        = string
  default     = ""
}

variable "domain_names" {
  description = "List of domain names for the certificate"
  type        = list(string)
  default     = []
}

variable "attach_custom_domain" {
  description = "Whether to attach custom domain (only after certificate validation)"
  type        = bool
  default     = false
}

variable "tags" {
  description = "Tags to apply to resources"
  type        = map(string)
  default     = {}
}

variable "ecr_repository_arn" {
  description = "ARN of the ECR repository for private registry access"
  type        = string
}