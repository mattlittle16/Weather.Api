using AutoFixture.Xunit2;
using Core.DTOs;
using Core.Interfaces;
using Infrastructure.Repositories;
using Moq;

namespace UnitTests.Repositories;

public class WeatherRepositoryTests
{
    [Theory]
    [AutoMoqData]
    public async Task GetWeatherAsync_CallsOpenWeatherApiAndReturnsResult
    (
        [Frozen] Mock<IOpenWeatherApi> openWeatherApiMock,
        string latitude,
        string longitude,
        OpenWeatherResponse expectedResponse
    )
    {
        //Arrange
        openWeatherApiMock.Setup(x => x.GetWeatherInfoAsync(latitude, longitude)).ReturnsAsync(expectedResponse);
        var repository = new WeatherRepository(openWeatherApiMock.Object);

        //Act
        var result = await repository.GetWeatherAsync(latitude, longitude);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse, result);
        openWeatherApiMock.Verify(x => x.GetWeatherInfoAsync(latitude, longitude), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task GetWeatherAsync_WhenApiThrowsException_PropagatesException
    (
        [Frozen] Mock<IOpenWeatherApi> openWeatherApiMock,
        string latitude,
        string longitude
    )
    {
        //Arrange
        var expectedException = new Exception("API Error");
        openWeatherApiMock.Setup(x => x.GetWeatherInfoAsync(latitude, longitude)).ThrowsAsync(expectedException);
        var repository = new WeatherRepository(openWeatherApiMock.Object);

        //Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => repository.GetWeatherAsync(latitude, longitude));
        Assert.Equal(expectedException.Message, exception.Message);
        openWeatherApiMock.Verify(x => x.GetWeatherInfoAsync(latitude, longitude), Times.Once);
    }
}
