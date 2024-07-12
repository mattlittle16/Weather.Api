using AutoFixture.Xunit2;
using Castle.Core.Logging;
using Core.Interfaces;
using Core.Models;
using Core.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Api.Controllers;

namespace UnitTests;

public class GeocodeControllerTests
{
    [Theory]
    [AutoMoqData]
    public void Get_ReturnsValidGeocode(
        [Frozen] Mock<IWeatherService> weatherServiceMock, 
        [Frozen] Mock<ILogger<GeocodeController>> loggerMock,
        string city, 
        string state, 
        string postalCode, 
        Geocode serviceResponse
    )
    {
        //Arrange 
        weatherServiceMock.Setup(x => x.GetGeocodeAsync(city, state, postalCode)).ReturnsAsync(serviceResponse);
        var controller = new GeocodeController(loggerMock.Object, weatherServiceMock.Object);
        
        //Act 
        var response = controller.Get(city, state, postalCode);

        //Assert
        var result = response.Result;

        Assert.NotNull(result);
        Assert.True(((OkObjectResult)result).StatusCode == 200);
    }
}