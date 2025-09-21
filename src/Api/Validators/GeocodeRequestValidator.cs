using Core.RequestModels;
using FluentValidation;

namespace Api.Validators
{
    public class GeocodeRequestValidator : AbstractValidator<GeocodeRequestModel>
    {
        public GeocodeRequestValidator()
        {
            // City rules - only required when PostalCode is not provided
            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("city must not be empty when postal code is not provided")
                .When(x => string.IsNullOrEmpty(x.PostalCode));
                
            RuleFor(x => x.City)
                .MaximumLength(150)
                .WithMessage("city name must be less than 150 characters")
                .When(x => !string.IsNullOrEmpty(x.City));

            // State rules - only required when PostalCode is not provided
            RuleFor(x => x.State)
                .NotEmpty()
                .WithMessage("state must not be empty when postal code is not provided")
                .When(x => string.IsNullOrEmpty(x.PostalCode));
                
            RuleFor(x => x.State)
                .MaximumLength(2)
                .WithMessage("state code must be 2 characters")
                .When(x => !string.IsNullOrEmpty(x.State));

            // PostalCode rules - format validation when provided
            RuleFor(x => x.PostalCode)
                .MaximumLength(5)
                .WithMessage("postal code must be 5 digit US postal code")
                .When(x => !string.IsNullOrEmpty(x.PostalCode));

            // CountryCode rules - ALWAYS required
            RuleFor(x => x.CountryCode)
                .NotEmpty()
                .WithMessage("country code must not be empty")
                .MaximumLength(2)
                .WithMessage("country code must be 2 characters");

            // Overall validation - must provide either PostalCode OR (City + State)
            RuleFor(x => x)
                .Must(x => !string.IsNullOrEmpty(x.PostalCode) || 
                          (!string.IsNullOrEmpty(x.City) && !string.IsNullOrEmpty(x.State)))
                .WithMessage("Either postal code must be provided, or both city and state must be provided")
                .WithName("LocationIdentifier");
        }        
    }
}