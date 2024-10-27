using Core.RequestModels;
using FluentValidation;

namespace Api.Validators
{
    public class GeocodeRequestValidator : AbstractValidator<GeocodeRequestModel>
    {
        public GeocodeRequestValidator()
        {
            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("city must not be empty")
                .MaximumLength(150)
                .WithMessage("city name must be less than 150 characters");

            RuleFor(x => x.State)
                .NotEmpty()
                .WithMessage("state must not be empty")
                .MaximumLength(2)
                .WithMessage("state code must be 2 characters");

            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .WithMessage("postal code must not be empty")
                .MaximumLength(5)
                .WithMessage("postal code must 5 digit US postal code");
        }        
    }
}