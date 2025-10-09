using AutoFixture.Xunit2;
using Core.Decorators;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Repositories;

public class CachedGeocodeRepositoryTests
{
    #region GetGeocodeAsync Tests

    [Theory]
    [AutoMoqData]
    public async Task GetGeocodeAsync_WithCacheHit_ReturnsCachedResultAndDoesNotCallRepository
    (
        [Frozen] Mock<IGeocodeRepository> geocodeRepositoryMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<CachedGeocodeRepository>> loggerMock,
        string city,
        string state,
        string countryCode,
        Geocode expectedGeocode
    )
    {
        //Arrange
        object cachedValue = expectedGeocode;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue!)).Returns(true);
        var cachedRepository = new CachedGeocodeRepository(geocodeRepositoryMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act
        var result = await cachedRepository.GetGeocodeAsync(city, state, countryCode);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedGeocode, result);
        geocodeRepositoryMock.Verify(x => x.GetGeocodeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        memoryCacheMock.Verify(x => x.TryGetValue(It.IsAny<object>(), out cachedValue!), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task GetGeocodeAsync_WithCacheMiss_CallsRepositoryAndCachesResult
    (
        [Frozen] Mock<IGeocodeRepository> geocodeRepositoryMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<CachedGeocodeRepository>> loggerMock,
        string city,
        string state,
        string countryCode,
        Geocode apiResponse
    )
    {
        //Arrange
        object? cachedValue = null;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue!)).Returns(false);
        geocodeRepositoryMock.Setup(x => x.GetGeocodeAsync(city, state, countryCode)).ReturnsAsync(apiResponse);

        var cacheEntryMock = new Mock<ICacheEntry>();
        memoryCacheMock.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns(cacheEntryMock.Object);

        var cachedRepository = new CachedGeocodeRepository(geocodeRepositoryMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act
        var result = await cachedRepository.GetGeocodeAsync(city, state, countryCode);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(apiResponse, result);
        geocodeRepositoryMock.Verify(x => x.GetGeocodeAsync(city, state, countryCode), Times.Once);
        memoryCacheMock.Verify(x => x.TryGetValue(It.IsAny<object>(), out cachedValue), Times.Once);
        memoryCacheMock.Verify(x => x.CreateEntry(It.IsAny<object>()), Times.Once);
    }

    #endregion

    #region GetGeocodeAsyncByPostalCode Tests

    [Theory]
    [AutoMoqData]
    public async Task GetGeocodeAsyncByPostalCode_WithCacheHit_ReturnsCachedResultAndDoesNotCallRepository
    (
        [Frozen] Mock<IGeocodeRepository> geocodeRepositoryMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<CachedGeocodeRepository>> loggerMock,
        string postalCode,
        string countryCode,
        Geocode expectedGeocode
    )
    {
        //Arrange
        object cachedValue = expectedGeocode;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue!)).Returns(true);
        var cachedRepository = new CachedGeocodeRepository(geocodeRepositoryMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act
        var result = await cachedRepository.GetGeocodeAsyncByPostalCode(postalCode, countryCode);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedGeocode, result);
        geocodeRepositoryMock.Verify(x => x.GetGeocodeAsyncByPostalCode(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        memoryCacheMock.Verify(x => x.TryGetValue(It.IsAny<object>(), out cachedValue!), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task GetGeocodeAsyncByPostalCode_WithCacheMiss_CallsRepositoryAndCachesResult
    (
        [Frozen] Mock<IGeocodeRepository> geocodeRepositoryMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<CachedGeocodeRepository>> loggerMock,
        string postalCode,
        string countryCode,
        Geocode apiResponse
    )
    {
        //Arrange
        object? cachedValue = null;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue!)).Returns(false);
        geocodeRepositoryMock.Setup(x => x.GetGeocodeAsyncByPostalCode(postalCode, countryCode)).ReturnsAsync(apiResponse);

        var cacheEntryMock = new Mock<ICacheEntry>();
        memoryCacheMock.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns(cacheEntryMock.Object);

        var cachedRepository = new CachedGeocodeRepository(geocodeRepositoryMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act
        var result = await cachedRepository.GetGeocodeAsyncByPostalCode(postalCode, countryCode);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(apiResponse, result);
        geocodeRepositoryMock.Verify(x => x.GetGeocodeAsyncByPostalCode(postalCode, countryCode), Times.Once);
        memoryCacheMock.Verify(x => x.TryGetValue(It.IsAny<object>(), out cachedValue), Times.Once);
        memoryCacheMock.Verify(x => x.CreateEntry(It.IsAny<object>()), Times.Once);
    }

    #endregion

    #region ReverseGeocodeAsync Tests

    [Theory]
    [AutoMoqData]
    public async Task ReverseGeocodeAsync_WithCacheHit_ReturnsCachedResultAndDoesNotCallRepository
    (
        [Frozen] Mock<IGeocodeRepository> geocodeRepositoryMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<CachedGeocodeRepository>> loggerMock,
        string latitude,
        string longitude,
        ReverseGeocode expectedReverseGeocode
    )
    {
        //Arrange
        object cachedValue = expectedReverseGeocode;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue!)).Returns(true);
        var cachedRepository = new CachedGeocodeRepository(geocodeRepositoryMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act
        var result = await cachedRepository.ReverseGeocodeAsync(latitude, longitude);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedReverseGeocode, result);
        geocodeRepositoryMock.Verify(x => x.ReverseGeocodeAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        memoryCacheMock.Verify(x => x.TryGetValue(It.IsAny<object>(), out cachedValue!), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task ReverseGeocodeAsync_WithCacheMiss_CallsRepositoryAndCachesResult
    (
        [Frozen] Mock<IGeocodeRepository> geocodeRepositoryMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<CachedGeocodeRepository>> loggerMock,
        string latitude,
        string longitude,
        ReverseGeocode apiResponse
    )
    {
        //Arrange
        object? cachedValue = null;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue!)).Returns(false);
        geocodeRepositoryMock.Setup(x => x.ReverseGeocodeAsync(latitude, longitude)).ReturnsAsync(apiResponse);

        var cacheEntryMock = new Mock<ICacheEntry>();
        memoryCacheMock.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns(cacheEntryMock.Object);

        var cachedRepository = new CachedGeocodeRepository(geocodeRepositoryMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act
        var result = await cachedRepository.ReverseGeocodeAsync(latitude, longitude);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(apiResponse, result);
        geocodeRepositoryMock.Verify(x => x.ReverseGeocodeAsync(latitude, longitude), Times.Once);
        memoryCacheMock.Verify(x => x.TryGetValue(It.IsAny<object>(), out cachedValue), Times.Once);
        memoryCacheMock.Verify(x => x.CreateEntry(It.IsAny<object>()), Times.Once);
    }

    #endregion

    #region Exception Handling Tests

    [Theory]
    [AutoMoqData]
    public async Task GetGeocodeAsync_WhenRepositoryThrowsException_PropagatesException
    (
        [Frozen] Mock<IGeocodeRepository> geocodeRepositoryMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<CachedGeocodeRepository>> loggerMock,
        string city,
        string state,
        string countryCode
    )
    {
        //Arrange
        object? cachedValue = null;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue)).Returns(false);

        var expectedException = new Exception("Repository Error");
        geocodeRepositoryMock.Setup(x => x.GetGeocodeAsync(city, state, countryCode)).ThrowsAsync(expectedException);

        var cachedRepository = new CachedGeocodeRepository(geocodeRepositoryMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => cachedRepository.GetGeocodeAsync(city, state, countryCode));
        Assert.Equal(expectedException.Message, exception.Message);
        geocodeRepositoryMock.Verify(x => x.GetGeocodeAsync(city, state, countryCode), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task GetGeocodeAsyncByPostalCode_WhenRepositoryThrowsException_PropagatesException
    (
        [Frozen] Mock<IGeocodeRepository> geocodeRepositoryMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<CachedGeocodeRepository>> loggerMock,
        string postalCode,
        string countryCode
    )
    {
        //Arrange
        object? cachedValue = null;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue)).Returns(false);

        var expectedException = new Exception("Repository Error");
        geocodeRepositoryMock.Setup(x => x.GetGeocodeAsyncByPostalCode(postalCode, countryCode)).ThrowsAsync(expectedException);

        var cachedRepository = new CachedGeocodeRepository(geocodeRepositoryMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => cachedRepository.GetGeocodeAsyncByPostalCode(postalCode, countryCode));
        Assert.Equal(expectedException.Message, exception.Message);
        geocodeRepositoryMock.Verify(x => x.GetGeocodeAsyncByPostalCode(postalCode, countryCode), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task ReverseGeocodeAsync_WhenRepositoryThrowsException_PropagatesException
    (
        [Frozen] Mock<IGeocodeRepository> geocodeRepositoryMock,
        [Frozen] Mock<IMemoryCache> memoryCacheMock,
        [Frozen] Mock<ILogger<CachedGeocodeRepository>> loggerMock,
        string latitude,
        string longitude
    )
    {
        //Arrange
        object? cachedValue = null;
        memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue)).Returns(false);

        var expectedException = new Exception("Repository Error");
        geocodeRepositoryMock.Setup(x => x.ReverseGeocodeAsync(latitude, longitude)).ThrowsAsync(expectedException);

        var cachedRepository = new CachedGeocodeRepository(geocodeRepositoryMock.Object, memoryCacheMock.Object, loggerMock.Object);

        //Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => cachedRepository.ReverseGeocodeAsync(latitude, longitude));
        Assert.Equal(expectedException.Message, exception.Message);
        geocodeRepositoryMock.Verify(x => x.ReverseGeocodeAsync(latitude, longitude), Times.Once);
    }

    #endregion
}
