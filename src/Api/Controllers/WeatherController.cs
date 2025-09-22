using Core.Constants;
using Core.Interfaces;
using Core.RequestModels;
using Core.ResponseModels;
using Core.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Core.Models;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[EnableRateLimiting(Constants.RateLimitPolicy)]
public class WeatherController(ILogger<WeatherController> logger, IWeatherService weatherService, IValidator<WeatherRequestModel> validator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(WeatherResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status429TooManyRequests)]
    public async Task<IActionResult> Get([FromQuery] WeatherRequestModel requestModel)
    {
        using var scope = logger.BeginScope("WeatherController.Get - {0}", Guid.NewGuid());
        var validatorResult = await validator.ValidateAsync(requestModel);

        if (validatorResult.IsValid)
        {
            return Ok(new WeatherResponse(await weatherService.GetWeatherAsync(requestModel.Lat!, requestModel.Lon!)));
        }
        else 
        {
            return BadRequest(validatorResult.PrintErrors());
        }
    }
}
