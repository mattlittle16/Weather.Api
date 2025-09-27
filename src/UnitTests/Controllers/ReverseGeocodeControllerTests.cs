using Api.Controllers;
using Api.Validators;
using AutoFixture.Xunit2;
using Core.Interfaces;
using Core.Models;
using Core.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Controllers;

public class ReverseGeocodeControllerTests
{
    [Theory]
    [AutoMoqData]
    public async Task Get_WithValidLatLon_ReturnsValidReverseGeocode
    (
        [Frozen] Mock<IWeatherService> weatherServiceMock,
        [Frozen] Mock<ILogger<ReverseGeocodeController>> loggerMock,
        string latitude,
        string longitude,
        ReverseGeocode serviceResponse
    )
    {
        //Arrange 
        var validator = new ReverseGeocodeRequestValidator();
        weatherServiceMock.Setup(x => x.ReverseGeocodeAsync(latitude, longitude)).ReturnsAsync(serviceResponse);
        var controller = new ReverseGeocodeController(loggerMock.Object, weatherServiceMock.Object, validator);

        //Act 
        var response = await controller.Get(new ReverseGeocodeRequestModel(latitude, longitude));

        //Assert
        var result = response;

        Assert.NotNull(result);
        Assert.True(((OkObjectResult)result).StatusCode == 200);
        weatherServiceMock.Verify(x => x.ReverseGeocodeAsync(latitude, longitude), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task Get_WithEmptyLatitude_ThrowsFriendlyException
    (
        [Frozen] Mock<IWeatherService> weatherServiceMock,
        [Frozen] Mock<ILogger<ReverseGeocodeController>> loggerMock,
        string longitude
    )
    {
        //Arrange 
        var validator = new ReverseGeocodeRequestValidator();
        var controller = new ReverseGeocodeController(loggerMock.Object, weatherServiceMock.Object, validator);

        //Act & Assert
        await Assert.ThrowsAsync<FriendlyException>(async () => 
            await controller.Get(new ReverseGeocodeRequestModel(null, longitude)));
        
        weatherServiceMock.Verify(x => x.ReverseGeocodeAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Theory]
    [AutoMoqData]
    public async Task Get_WithEmptyLongitude_ThrowsFriendlyException
    (
        [Frozen] Mock<IWeatherService> weatherServiceMock,
        [Frozen] Mock<ILogger<ReverseGeocodeController>> loggerMock,
        string latitude
    )
    {
        //Arrange 
        var validator = new ReverseGeocodeRequestValidator();
        var controller = new ReverseGeocodeController(loggerMock.Object, weatherServiceMock.Object, validator);

        //Act & Assert
        await Assert.ThrowsAsync<FriendlyException>(async () => 
            await controller.Get(new ReverseGeocodeRequestModel(latitude, null)));
        
        weatherServiceMock.Verify(x => x.ReverseGeocodeAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }
}