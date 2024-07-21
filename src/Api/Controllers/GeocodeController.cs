using Core.Constants;
using Core.Enums;
using Core.Extensions;
using Core.Interfaces;
using Core.RequestModels;
using Core.ResponseModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[EnableRateLimiting(Constants.RateLimitPolicy)]
public class GeocodeController : ControllerBase
{
    private readonly ILogger<GeocodeController> _logger;
    private readonly IWeatherService _weatherService;
    private readonly IValidator<GeocodeRequestModel> _validator;

    public GeocodeController(ILogger<GeocodeController> logger, IWeatherService weatherService, IValidator<GeocodeRequestModel> validator)
    {
        _logger = logger;
        _weatherService = weatherService;
        _validator = validator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(GeocodeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status429TooManyRequests)]
    public async Task<IActionResult> Get([FromQuery] GeocodeRequestModel requestModel)
    {
        var validatorResult = await _validator.ValidateAsync(requestModel);                
        _logger.LogInformation($"Geocode request {requestModel.City} {requestModel.State} {requestModel.PostalCode}", LogTypeEnum.General);

        if (validatorResult.IsValid)
        {
            return Ok(new GeocodeResponse(await _weatherService.GetGeocodeAsync(requestModel.City, requestModel.State, requestModel.PostalCode)));
        }
        else 
        {
            return BadRequest(validatorResult.PrintErrors());
        }
    }
}
