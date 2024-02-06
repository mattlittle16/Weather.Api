using Core.Constants;
using Core.Interfaces;
using Core.Models;
using Core.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace WeatherApi.Controllers;

[ApiController]
[Route("[controller]")]
[EnableRateLimiting(Constants.RateLimitPolicy)]
public class HealthCheckController : ControllerBase
{        
    public HealthCheckController(ILogger<WeatherController> logger, IWeatherService weatherService)
    {        
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {              
       return Ok();
    }    
}
