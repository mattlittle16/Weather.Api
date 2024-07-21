using Api.Validators;
using Core.Constants;
using Core.Interfaces;
using Core.RequestModels;
using Core.ResponseModels;
using Core.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Refit;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[EnableRateLimiting(Constants.RateLimitPolicy)]
public class WeatherController : ControllerBase
{
    private readonly ILogger<WeatherController> _logger;
    private readonly IWeatherService _weatherService;
    private readonly IValidator<WeatherRequestModel> _validator;

    public WeatherController(ILogger<WeatherController> logger, IWeatherService weatherService, IValidator<WeatherRequestModel> validator)
    {
        _logger = logger;
        _weatherService = weatherService;
        _validator = validator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(WeatherResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status429TooManyRequests)]
    public async Task<IActionResult> Get([FromQuery] WeatherRequestModel requestModel)
    {
        var validatorResult = await _validator.ValidateAsync(requestModel);
        _logger.LogInformation($"Weather request {requestModel.Lat} - {requestModel.Lon}");

        if (validatorResult.IsValid)
        {
            return Ok(new WeatherResponse(await _weatherService.GetWeatherAsync(requestModel.Lat, requestModel.Lon)));
        }
        else 
        {
            return BadRequest(validatorResult.PrintErrors());
        }
    }
}
