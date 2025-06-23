using Core.RequestModels;
using FluentValidation;

namespace Api.Validators
{
    public class WeatherRequestValidator : AbstractValidator<WeatherRequestModel>
    {
        public WeatherRequestValidator()
        {
            RuleFor(x => x.Lat)
                .NotEmpty()
                .WithMessage("lat must not be empty")
                .Must(x => { decimal parseValue = 0M; return decimal.TryParse(x, out parseValue);  })
                .WithMessage("invalid latitude provided");

            RuleFor(x => x.Lon)
                .NotEmpty()
                .WithMessage("lon must not be empty")
                .Must(x => { decimal parseValue = 0M; return decimal.TryParse(x, out parseValue);  })
                .WithMessage("invalid longitude provided");
        }
    }
}