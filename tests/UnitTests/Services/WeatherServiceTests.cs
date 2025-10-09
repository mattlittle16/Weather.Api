using Api.ApplicationLogic;
using AutoFixture.Xunit2;
using Core.DTOs;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Services;

public class WeatherServiceTests
{
    [Theory]
    [AutoMoqData]
    public async Task GetGeocodeAsync_ByCityState_WithCacheHit_ReturnsCachedResult
    (
        [Frozen] Mock<IOpenWeatherApi> openWeatherApiMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<WeatherService>> loggerMock,
        string city,
        string state,
        string countryCode,
        Geocode expectedGeocode
    )
    {
        //Arrange
        object cachedValue = expectedGeocode;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue!)).Returns(true);
        var service = new WeatherService(openWeatherApiMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act
        var result = await service.GetGeocodeAsync(city, state, countryCode);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedGeocode, result);
        openWeatherApiMock.Verify(x => x.GetGeocodeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Theory]
    [AutoMoqData]
    public async Task GetGeocodeAsync_ByCityState_WithCacheMiss_CallsApiAndReturnsResult
    (
        [Frozen] Mock<IOpenWeatherApi> openWeatherApiMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<WeatherService>> loggerMock,
        string city,
        string state,
        string countryCode,
        Geocode apiResponse
    )
    {
        //Arrange
        object? cachedValue = null;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue)).Returns(false);
        openWeatherApiMock.Setup(x => x.GetGeocodeAsync(city, state, countryCode)).ReturnsAsync(apiResponse);
        var service = new WeatherService(openWeatherApiMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act
        var result = await service.GetGeocodeAsync(city, state, countryCode);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(apiResponse, result);
        openWeatherApiMock.Verify(x => x.GetGeocodeAsync(city, state, countryCode), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task GetGeocodeAsync_ByPostalCode_WithCacheHit_ReturnsCachedResult
    (
        [Frozen] Mock<IOpenWeatherApi> openWeatherApiMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<WeatherService>> loggerMock,
        string postalCode,
        string countryCode,
        Geocode expectedGeocode
    )
    {
        //Arrange
        object cachedValue = expectedGeocode;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue!)).Returns(true);
        var service = new WeatherService(openWeatherApiMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act
        var result = await service.GetGeocodeAsync(postalCode, countryCode);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedGeocode, result);
        openWeatherApiMock.Verify(x => x.GetGeocodeZipAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Theory]
    [AutoMoqData]
    public async Task GetGeocodeAsync_ByPostalCode_WithCacheMiss_CallsApiAndReturnsResult
    (
        [Frozen] Mock<IOpenWeatherApi> openWeatherApiMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<WeatherService>> loggerMock,
        string postalCode,
        string countryCode,
        Geocode apiResponse
    )
    {
        //Arrange
        object? cachedValue = null;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue)).Returns(false);
        openWeatherApiMock.Setup(x => x.GetGeocodeZipAsync(postalCode, countryCode)).ReturnsAsync(apiResponse);
        var service = new WeatherService(openWeatherApiMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act
        var result = await service.GetGeocodeAsync(postalCode, countryCode);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(apiResponse, result);
        openWeatherApiMock.Verify(x => x.GetGeocodeZipAsync(postalCode, countryCode), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task ReverseGeocodeAsync_WithCacheHit_ReturnsCachedResult
    (
        [Frozen] Mock<IOpenWeatherApi> openWeatherApiMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<WeatherService>> loggerMock,
        string latitude,
        string longitude,
        ReverseGeocode expectedReverseGeocode
    )
    {
        //Arrange
        object cachedValue = expectedReverseGeocode;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue!)).Returns(true);
        var service = new WeatherService(openWeatherApiMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act
        var result = await service.ReverseGeocodeAsync(latitude, longitude);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedReverseGeocode, result);
        openWeatherApiMock.Verify(x => x.ReverseGeocodeAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Theory]
    [AutoMoqData]
    public async Task ReverseGeocodeAsync_WithCacheMiss_CallsApiAndReturnsResult
    (
        [Frozen] Mock<IOpenWeatherApi> openWeatherApiMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<WeatherService>> loggerMock,
        string latitude,
        string longitude,
        ReverseGeocode apiResponse
    )
    {
        //Arrange
        object? cachedValue = null;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue)).Returns(false);
        openWeatherApiMock.Setup(x => x.ReverseGeocodeAsync(latitude, longitude)).ReturnsAsync(apiResponse);
        var service = new WeatherService(openWeatherApiMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act
        var result = await service.ReverseGeocodeAsync(latitude, longitude);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(apiResponse, result);
        openWeatherApiMock.Verify(x => x.ReverseGeocodeAsync(latitude, longitude), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task GetWeatherAsync_WithCacheHit_ReturnsCachedResult
    (
        [Frozen] Mock<IOpenWeatherApi> openWeatherApiMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<WeatherService>> loggerMock,
        string latitude,
        string longitude,
        OpenWeatherResponse expectedWeather
    )
    {
        //Arrange
        object cachedValue = expectedWeather;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue!)).Returns(true);
        var service = new WeatherService(openWeatherApiMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act
        var result = await service.GetWeatherAsync(latitude, longitude);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedWeather, result);
        openWeatherApiMock.Verify(x => x.GetWeatherInfoAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Theory]
    [AutoMoqData]
    public async Task GetWeatherAsync_WithCacheMiss_CallsApiAndReturnsResult
    (
        [Frozen] Mock<IOpenWeatherApi> openWeatherApiMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<WeatherService>> loggerMock,
        string latitude,
        string longitude,
        OpenWeatherResponse apiResponse
    )
    {
        //Arrange
        object? cachedValue = null;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue)).Returns(false);
        openWeatherApiMock.Setup(x => x.GetWeatherInfoAsync(latitude, longitude)).ReturnsAsync(apiResponse);
        var service = new WeatherService(openWeatherApiMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act
        var result = await service.GetWeatherAsync(latitude, longitude);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(apiResponse, result);
        openWeatherApiMock.Verify(x => x.GetWeatherInfoAsync(latitude, longitude), Times.Once);
    }
}