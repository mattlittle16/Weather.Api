using Core.ApplicationLogic;
using AutoFixture.Xunit2;
using Core.Interfaces;
using Core.Models;
using Moq;

namespace UnitTests.Services;

public class GeocodeServiceTests
{
    [Theory]
    [AutoMoqData]
    public async Task GetGeocodeAsync_ByCityState_CallsRepositoryAndReturnsResult
    (
        [Frozen] Mock<IGeocodeRepository> geocodeRepositoryMock,
        string city,
        string state,
        string countryCode,
        Geocode expectedGeocode
    )
    {
        //Arrange
        geocodeRepositoryMock.Setup(x => x.GetGeocodeAsync(city, state, countryCode)).ReturnsAsync(expectedGeocode);
        var service = new GeocodeService(geocodeRepositoryMock.Object);

        //Act
        var result = await service.GetGeocodeAsync(city, state, countryCode);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedGeocode, result);
        geocodeRepositoryMock.Verify(x => x.GetGeocodeAsync(city, state, countryCode), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task GetGeocodeAsync_ByPostalCode_CallsRepositoryAndReturnsResult
    (
        [Frozen] Mock<IGeocodeRepository> geocodeRepositoryMock,
        string postalCode,
        string countryCode,
        Geocode expectedGeocode
    )
    {
        //Arrange
        geocodeRepositoryMock.Setup(x => x.GetGeocodeAsyncByPostalCode(postalCode, countryCode)).ReturnsAsync(expectedGeocode);
        var service = new GeocodeService(geocodeRepositoryMock.Object);

        //Act
        var result = await service.GetGeocodeAsync(postalCode, countryCode);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedGeocode, result);
        geocodeRepositoryMock.Verify(x => x.GetGeocodeAsyncByPostalCode(postalCode, countryCode), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task ReverseGeocodeAsync_CallsRepositoryAndReturnsResult
    (
        [Frozen] Mock<IGeocodeRepository> geocodeRepositoryMock,
        string latitude,
        string longitude,
        ReverseGeocode expectedReverseGeocode
    )
    {
        //Arrange
        geocodeRepositoryMock.Setup(x => x.ReverseGeocodeAsync(latitude, longitude)).ReturnsAsync(expectedReverseGeocode);
        var service = new GeocodeService(geocodeRepositoryMock.Object);

        //Act
        var result = await service.ReverseGeocodeAsync(latitude, longitude);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedReverseGeocode, result);
        geocodeRepositoryMock.Verify(x => x.ReverseGeocodeAsync(latitude, longitude), Times.Once);
    }
}