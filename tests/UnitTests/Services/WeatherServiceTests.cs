using Core.ApplicationLogic;
using AutoFixture.Xunit2;
using Core.DTOs;
using Core.Interfaces;
using Moq;

namespace UnitTests.Services;

public class WeatherServiceTests
{
    [Theory]
    [AutoMoqData]
    public async Task GetWeatherAsync_CallsRepositoryAndReturnsResult
    (
        [Frozen] Mock<IWeatherRepository> weatherRepositoryMock,
        string latitude,
        string longitude,
        OpenWeatherResponse expectedWeather
    )
    {
        //Arrange
        weatherRepositoryMock.Setup(x => x.GetWeatherAsync(latitude, longitude)).ReturnsAsync(expectedWeather);
        var service = new WeatherService(weatherRepositoryMock.Object);

        //Act
        var result = await service.GetWeatherAsync(latitude, longitude);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedWeather, result);
        weatherRepositoryMock.Verify(x => x.GetWeatherAsync(latitude, longitude), Times.Once);
    }
}