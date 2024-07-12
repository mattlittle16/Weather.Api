using Core.Constants;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers;

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
