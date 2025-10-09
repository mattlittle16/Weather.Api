using System.Text.Json;
using Core.Constants;
using Core.Extensions;
using Core.Interfaces;
using Core.Models;
using Core.RequestModels;
using Core.ResponseModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[EnableRateLimiting(Constants.RateLimitPolicy)]
public class GeocodeController(ILogger<GeocodeController> logger, IGeocodeService geocodeService, IValidator<GeocodeRequestModel> validator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(Geocode), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status429TooManyRequests)]
    public async Task<IActionResult> Get([FromQuery] GeocodeRequestModel requestModel)
    {
        using var scope = logger.BeginScope("GeocodeController.Get - {0}", Guid.NewGuid());
        var validatorResult = await validator.ValidateAsync(requestModel);

        if (validatorResult.IsValid)
        {
            return requestModel switch
            {
                { PostalCode: not null and not "" } => Ok(await geocodeService.GetGeocodeAsync(requestModel.PostalCode!, requestModel.CountryCode!)),
                { City: not null and not "" } => Ok(await geocodeService.GetGeocodeAsync(requestModel.City!, requestModel.State!, requestModel.CountryCode!)),
                _ => throw new NotImplementedException()
            };
        }
        else
        {
            throw new FriendlyException("Validation Error", 400, JsonSerializer.Serialize(validatorResult.PrintErrors()));
        }
    }
}