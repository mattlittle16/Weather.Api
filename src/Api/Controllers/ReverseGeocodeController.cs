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
public class ReverseGeocodeController(ILogger<ReverseGeocodeController> logger, IWeatherService weatherService, IValidator<ReverseGeocodeRequestModel> validator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ReverseGeocode), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status429TooManyRequests)]
    public async Task<IActionResult> Get([FromQuery] ReverseGeocodeRequestModel requestModel)
    {
        using var scope = logger.BeginScope("ReverseGeocodeController.Get - {0}", Guid.NewGuid());
        var validatorResult = await validator.ValidateAsync(requestModel);

        if (validatorResult.IsValid)
        {
            return Ok(await weatherService.ReverseGeocodeAsync(requestModel.Lat!, requestModel.Lon!));
        }
        else
        {
            throw new FriendlyException("Validation Error", 400, JsonSerializer.Serialize(validatorResult.PrintErrors()));
        }
    }
}