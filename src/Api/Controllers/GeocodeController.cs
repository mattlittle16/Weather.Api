using Core.Constants;
using Core.Extensions;
using Core.Interfaces;
using Core.RequestModels;
using Core.ResponseModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[EnableRateLimiting(Constants.RateLimitPolicy)]
public class GeocodeController(ILogger<GeocodeController> logger, IWeatherService weatherService, IValidator<GeocodeRequestModel> validator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GeocodeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status429TooManyRequests)]
    public async Task<IActionResult> Get([FromQuery] GeocodeRequestModel requestModel)
    {
        using var scope = logger.BeginScope("GeocodeController.Get - {0}", Guid.NewGuid());
        var validatorResult = await validator.ValidateAsync(requestModel);
        
        if (validatorResult.IsValid)
        {
            return Ok(new GeocodeResponse(await weatherService.GetGeocodeAsync(requestModel.City!, requestModel.State!, requestModel.PostalCode!)));
        }
        else 
        {
            return BadRequest(validatorResult.PrintErrors());
        }
    }
}