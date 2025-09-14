output "record_name" {
  description = "Name of the DNS CNAME record"
  value       = aws_route53_record.subdomain.name
}

output "record_fqdn" {
  description = "Fully qualified domain name of the subdomain"
  value       = aws_route53_record.subdomain.fqdn
}

output "cname_target" {
  description = "Target hostname of the CNAME record"
  value       = tolist(aws_route53_record.subdomain.records)[0]
}

output "lightsail_hostname" {
  description = "Extracted Lightsail hostname from service URL"
  value       = local.lightsail_hostname
}