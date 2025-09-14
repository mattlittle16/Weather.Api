resource "aws_lightsail_container_service" "weather_api" {
  name  = var.service_name
  power = var.container_power
  scale = var.container_scale

  # Private registry access for ECR
  private_registry_access {
    ecr_image_puller_role {
      is_active = true
    }
  }

  # SSL certificate configuration (only when validation is complete)
  dynamic "public_domain_names" {
    for_each = var.attach_custom_domain && var.certificate_name != "" ? [1] : []
    content {
      certificate {
        certificate_name = var.certificate_name
        domain_names     = var.domain_names
      }
    }
  }

  tags = merge(var.tags, {
    Name        = var.service_name
    Environment = var.environment_name
  })
}

# Container deployment version
resource "aws_lightsail_container_service_deployment_version" "weather_api" {
  service_name = aws_lightsail_container_service.weather_api.name

  container {
    container_name = "weather-api"
    image         = var.container_image

    environment = {
      ASPNETCORE_ENVIRONMENT                   = var.environment_name
      ASPNETCORE_URLS                         = "http://+:80"
      AppSettings__OpenWeatherApiKey          = var.openweather_api_key
      AppSettings__OpenWeatherApiBaseUrl      = var.openweather_base_url
      AppSettings__XApiKey                    = var.api_key
      AppSettings__RateLimit                  = tostring(var.rate_limit)
      AppSettings__RateLimitTimeInMinutes     = tostring(var.rate_limit_time_minutes)
      AppSettings__DailyOpenWeatherApiLimit   = tostring(var.daily_api_limit)
    }

    ports = {
      "80" = "HTTP"
    }
  }

  public_endpoint {
    container_name = "weather-api"
    container_port = 80

    health_check {
      healthy_threshold   = 2
      unhealthy_threshold = 2
      timeout_seconds     = 10
      interval_seconds    = 30
      path               = "/healthcheck"
      success_codes      = "200"
    }
  }
}

# Note: Custom domain attachment will be done in a separate Terraform apply
# after SSL certificate validation is complete