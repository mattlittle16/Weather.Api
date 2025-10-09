using AutoFixture.Xunit2;
using Core.Decorators;
using Core.DTOs;
using Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Repositories;

public class CachedWeatherRepositoryTests
{
    [Theory]
    [AutoMoqData]
    public async Task GetWeatherAsync_WithCacheHit_ReturnsCachedResultAndDoesNotCallRepository
    (
        [Frozen] Mock<IWeatherRepository> weatherRepositoryMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<CachedWeatherRepository>> loggerMock,
        string latitude,
        string longitude,
        OpenWeatherResponse expectedWeather
    )
    {
        //Arrange
        object cachedValue = expectedWeather;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue!)).Returns(true);
        var cachedRepository = new CachedWeatherRepository(weatherRepositoryMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act
        var result = await cachedRepository.GetWeatherAsync(latitude, longitude);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedWeather, result);
        weatherRepositoryMock.Verify(x => x.GetWeatherAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        memoryCacheMock.Verify(x => x.TryGetValue(It.IsAny<object>(), out cachedValue!), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task GetWeatherAsync_WithCacheMiss_CallsRepositoryAndCachesResult
    (
        [Frozen] Mock<IWeatherRepository> weatherRepositoryMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<CachedWeatherRepository>> loggerMock,
        string latitude,
        string longitude,
        OpenWeatherResponse apiResponse
    )
    {
        //Arrange
        object? cachedValue = null;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue!)).Returns(false);
        weatherRepositoryMock.Setup(x => x.GetWeatherAsync(latitude, longitude)).ReturnsAsync(apiResponse);

        var cacheEntryMock = new Mock<ICacheEntry>();
        memoryCacheMock.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns(cacheEntryMock.Object);

        var cachedRepository = new CachedWeatherRepository(weatherRepositoryMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act
        var result = await cachedRepository.GetWeatherAsync(latitude, longitude);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(apiResponse, result);
        weatherRepositoryMock.Verify(x => x.GetWeatherAsync(latitude, longitude), Times.Once);
        memoryCacheMock.Verify(x => x.TryGetValue(It.IsAny<object>(), out cachedValue), Times.Once);
        memoryCacheMock.Verify(x => x.CreateEntry(It.IsAny<object>()), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task GetWeatherAsync_WithCacheHitButNullValue_CallsRepository
    (
        [Frozen] Mock<IWeatherRepository> weatherRepositoryMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<CachedWeatherRepository>> loggerMock,
        string latitude,
        string longitude,
        OpenWeatherResponse apiResponse
    )
    {
        //Arrange
        object cachedValue = null!;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue!)).Returns(true);
        weatherRepositoryMock.Setup(x => x.GetWeatherAsync(latitude, longitude)).ReturnsAsync(apiResponse);

        var cacheEntryMock = new Mock<ICacheEntry>();
        memoryCacheMock.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns(cacheEntryMock.Object);

        var cachedRepository = new CachedWeatherRepository(weatherRepositoryMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act
        var result = await cachedRepository.GetWeatherAsync(latitude, longitude);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(apiResponse, result);
        weatherRepositoryMock.Verify(x => x.GetWeatherAsync(latitude, longitude), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task GetWeatherAsync_WhenRepositoryThrowsException_PropagatesException
    (
        [Frozen] Mock<IWeatherRepository> weatherRepositoryMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<CachedWeatherRepository>> loggerMock,
        string latitude,
        string longitude
    )
    {
        //Arrange
        object? cachedValue = null;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue!)).Returns(false);

        var expectedException = new Exception("Repository Error");
        weatherRepositoryMock.Setup(x => x.GetWeatherAsync(latitude, longitude)).ThrowsAsync(expectedException);

        var cachedRepository = new CachedWeatherRepository(weatherRepositoryMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => cachedRepository.GetWeatherAsync(latitude, longitude));
        Assert.Equal(expectedException.Message, exception.Message);
        weatherRepositoryMock.Verify(x => x.GetWeatherAsync(latitude, longitude), Times.Once);
    }
}
