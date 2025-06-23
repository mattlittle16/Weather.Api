using System.ComponentModel.DataAnnotations;
using AutoFixture.Xunit2;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Api.Controllers;
using Api.Validators;
using AutoFixture;
using Core.RequestModels;
using FluentValidation;

namespace UnitTests.Controllers;

public class GeocodeControllerTests
{
    [Theory]
    [AutoMoqData]
    public async Task Get_ReturnsValidGeocode
    (
        [Frozen] Mock<IWeatherService> weatherServiceMock, 
        [Frozen] Mock<ILogger<GeocodeController>> loggerMock,
        string city, 
        [MaxLength(2)]
        string state, 
        [MaxLength(5)]
        string postalCode, 
        Geocode serviceResponse
    )
    {
        //Arrange 
        var validator = new GeocodeRequestValidator();
        weatherServiceMock.Setup(x => x.GetGeocodeAsync(city, state, postalCode)).ReturnsAsync(serviceResponse);
        var controller = new GeocodeController(loggerMock.Object, weatherServiceMock.Object, validator);
        
        //Act 
        var response = await controller.Get(new GeocodeRequestModel { City = city, PostalCode = postalCode, State = state});

        //Assert
        var result = response;

        Assert.NotNull(result);
        Assert.True(((OkObjectResult)result).StatusCode == 200);
    }
}