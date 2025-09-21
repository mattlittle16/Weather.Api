using Core.RequestModels;
using FluentValidation;

namespace Api.Validators
{
    public class ReverseGeocodeRequestValidator : AbstractValidator<ReverseGeocodeRequestModel>
    {
        public ReverseGeocodeRequestValidator()
        {
            RuleFor(x => x.Lat)
                .NotEmpty()
                .WithMessage("latitude must not be empty");

            RuleFor(x => x.Lon)
                .NotEmpty()
                .WithMessage("longitude must not be empty");
        }
    }
}