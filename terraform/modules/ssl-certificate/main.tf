resource "aws_lightsail_certificate" "weather_api" {
  name                      = var.certificate_name
  domain_name              = var.domain_name
  subject_alternative_names = var.subject_alternative_names

  tags = merge(var.tags, {
    Name = var.certificate_name
  })
}

# Automatically create DNS validation records
resource "aws_route53_record" "certificate_validation" {
  for_each = {
    for dvo in aws_lightsail_certificate.weather_api.domain_validation_options : dvo.domain_name => {
      name   = dvo.resource_record_name
      record = dvo.resource_record_value
      type   = dvo.resource_record_type
    }
  }

  allow_overwrite = true
  name            = each.value.name
  records         = [each.value.record]
  ttl             = 60
  type            = each.value.type
  zone_id         = var.hosted_zone_id
}