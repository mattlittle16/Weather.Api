variable "hosted_zone_id" {
  description = "Route53 hosted zone ID for the parent domain"
  type        = string
}

variable "subdomain" {
  description = "Subdomain to create (e.g., weather-api)"
  type        = string
}

variable "domain_name" {
  description = "Full domain name (e.g., weather-api.mattlittle.me)"
  type        = string
}

variable "lightsail_service_name" {
  description = "Name of the Lightsail container service"
  type        = string
}

variable "lightsail_default_domain" {
  description = "Default domain of the Lightsail container service"
  type        = string
}

variable "ttl" {
  description = "TTL for DNS records"
  type        = number
  default     = 300
}

variable "tags" {
  description = "Tags to apply to resources"
  type        = map(string)
  default     = {}
}