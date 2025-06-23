using Core.Constants;
using Core.Interfaces;
using Core.RequestModels;
using Core.ResponseModels;
using Core.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[EnableRateLimiting(Constants.RateLimitPolicy)]
public class WeatherController(ILogger<WeatherController> logger, IWeatherService weatherService, IValidator<WeatherRequestModel> validator) : ControllerBase
{
    private readonly ILogger<WeatherController> _logger = logger;
    private readonly IWeatherService _weatherService = weatherService;
    private readonly IValidator<WeatherRequestModel> _validator = validator;

    [HttpGet]
    [ProducesResponseType(typeof(WeatherResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status429TooManyRequests)]
    public async Task<IActionResult> Get([FromQuery] WeatherRequestModel requestModel)
    {
        using var scope = _logger.BeginScope("WeatherController.Get - {0}", Guid.NewGuid());
        var validatorResult = await _validator.ValidateAsync(requestModel);
        _logger.LogInformation($"Weather request {requestModel.Lat} - {requestModel.Lon}");

        if (validatorResult.IsValid)
        {
            return Ok(new WeatherResponse(await _weatherService.GetWeatherAsync(requestModel.Lat!, requestModel.Lon!)));
        }
        else 
        {
            return BadRequest(validatorResult.PrintErrors());
        }
    }
}
