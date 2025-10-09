using Api.Controllers;
using Api.Validators;
using AutoFixture.Xunit2;
using Core.DTOs;
using Core.Interfaces;
using Core.Models;
using Core.RequestModels;
using Core.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Controllers;

public class WeatherControllerTests
{
    [Theory]
    [AutoMoqData]
    public async Task Get_WithValidLatLon_ReturnsValidWeather
    (
        [Frozen] Mock<IWeatherService> weatherServiceMock,
        [Frozen] Mock<ILogger<WeatherController>> loggerMock,
        OpenWeatherResponse serviceResponse
    )
    {
        //Arrange 
        var latitude = "40.7128";
        var longitude = "-74.0060";
        var validator = new WeatherRequestValidator();
        weatherServiceMock.Setup(x => x.GetWeatherAsync(latitude, longitude)).ReturnsAsync(serviceResponse);
        var controller = new WeatherController(loggerMock.Object, weatherServiceMock.Object, validator);

        //Act 
        var response = await controller.Get(new WeatherRequestModel(latitude, longitude));

        //Assert
        var result = response;

        Assert.NotNull(result);
        Assert.True(((OkObjectResult)result).StatusCode == 200);
        Assert.IsType<WeatherResponse>(((OkObjectResult)result).Value);
        weatherServiceMock.Verify(x => x.GetWeatherAsync(latitude, longitude), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task Get_WithEmptyLatitude_ReturnsBadRequest
    (
        [Frozen] Mock<IWeatherService> weatherServiceMock,
        [Frozen] Mock<ILogger<WeatherController>> loggerMock,
        string longitude
    )
    {
        //Arrange 
        var validator = new WeatherRequestValidator();
        var controller = new WeatherController(loggerMock.Object, weatherServiceMock.Object, validator);

        //Act 
        var response = await controller.Get(new WeatherRequestModel(null, longitude));

        //Assert
        Assert.NotNull(response);
        Assert.IsType<BadRequestObjectResult>(response);
        Assert.True(((BadRequestObjectResult)response).StatusCode == 400);
        weatherServiceMock.Verify(x => x.GetWeatherAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Theory]
    [AutoMoqData]
    public async Task Get_WithEmptyLongitude_ReturnsBadRequest
    (
        [Frozen] Mock<IWeatherService> weatherServiceMock,
        [Frozen] Mock<ILogger<WeatherController>> loggerMock,
        string latitude
    )
    {
        //Arrange 
        var validator = new WeatherRequestValidator();
        var controller = new WeatherController(loggerMock.Object, weatherServiceMock.Object, validator);

        //Act 
        var response = await controller.Get(new WeatherRequestModel(latitude, null));

        //Assert
        Assert.NotNull(response);
        Assert.IsType<BadRequestObjectResult>(response);
        Assert.True(((BadRequestObjectResult)response).StatusCode == 400);
        weatherServiceMock.Verify(x => x.GetWeatherAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Theory]
    [AutoMoqData]
    public async Task Get_WithInvalidLatitude_ReturnsBadRequest
    (
        [Frozen] Mock<IWeatherService> weatherServiceMock,
        [Frozen] Mock<ILogger<WeatherController>> loggerMock,
        string longitude
    )
    {
        //Arrange 
        var validator = new WeatherRequestValidator();
        var controller = new WeatherController(loggerMock.Object, weatherServiceMock.Object, validator);

        //Act 
        var response = await controller.Get(new WeatherRequestModel("invalid_lat", longitude));

        //Assert
        Assert.NotNull(response);
        Assert.IsType<BadRequestObjectResult>(response);
        Assert.True(((BadRequestObjectResult)response).StatusCode == 400);
        weatherServiceMock.Verify(x => x.GetWeatherAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Theory]
    [AutoMoqData]
    public async Task Get_WithInvalidLongitude_ReturnsBadRequest
    (
        [Frozen] Mock<IWeatherService> weatherServiceMock,
        [Frozen] Mock<ILogger<WeatherController>> loggerMock,
        string latitude
    )
    {
        //Arrange 
        var validator = new WeatherRequestValidator();
        var controller = new WeatherController(loggerMock.Object, weatherServiceMock.Object, validator);

        //Act 
        var response = await controller.Get(new WeatherRequestModel(latitude, "invalid_lon"));

        //Assert
        Assert.NotNull(response);
        Assert.IsType<BadRequestObjectResult>(response);
        Assert.True(((BadRequestObjectResult)response).StatusCode == 400);
        weatherServiceMock.Verify(x => x.GetWeatherAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }
}