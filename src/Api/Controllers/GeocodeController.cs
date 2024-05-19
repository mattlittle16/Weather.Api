using Core.Constants;
using Core.Enums;
using Core.Interfaces;
using Core.Models;
using Core.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace WeatherApi.Controllers;

[ApiController]
[Route("[controller]")]
[EnableRateLimiting(Constants.RateLimitPolicy)]
public class GeocodeController : ControllerBase
{
    private readonly ILogger<WeatherController> _logger;
    private readonly IWeatherService _weatherService;

    public GeocodeController(ILogger<WeatherController> logger, IWeatherService weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(GeocodeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status429TooManyRequests)]
    public async Task<IActionResult> Get(string city, string state, string postalCode)
    {
        _logger.LogInformation($"Geocode request {city} {state} {postalCode}", LogTypeEnum.General);
        return Ok(new GeocodeResponse(await _weatherService.GetGeocodeAsync(city, state, postalCode)));
    }
}
