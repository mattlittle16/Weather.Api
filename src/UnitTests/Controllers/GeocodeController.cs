using AutoFixture.Xunit2;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Api.Controllers;
using AutoFixture;

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
        string state, 
        string postalCode, 
        Geocode serviceResponse
    )
    {
        //Arrange 
        weatherServiceMock.Setup(x => x.GetGeocodeAsync(city, state, postalCode)).ReturnsAsync(serviceResponse);
        var controller = new GeocodeController(loggerMock.Object, weatherServiceMock.Object);
        
        //Act 
        var response = await controller.Get(city, state, postalCode);

        //Assert
        var result = response;

        Assert.NotNull(result);
        Assert.True(((OkObjectResult)result).StatusCode == 200);
    }
}