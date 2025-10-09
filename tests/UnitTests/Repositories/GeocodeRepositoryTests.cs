using AutoFixture.Xunit2;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Repositories;
using Moq;

namespace UnitTests.Repositories;

public class GeocodeRepositoryTests
{
    [Theory]
    [AutoMoqData]
    public async Task GetGeocodeAsync_ByCityStateCountry_CallsOpenWeatherApiAndReturnsResult
    (
        [Frozen] Mock<IOpenWeatherApi> openWeatherApiMock,
        string city,
        string state,
        string countryCode,
        Geocode expectedGeocode
    )
    {
        //Arrange
        openWeatherApiMock.Setup(x => x.GetGeocodeAsync(city, state, countryCode)).ReturnsAsync(expectedGeocode);
        var repository = new GeocodeRepository(openWeatherApiMock.Object);

        //Act
        var result = await repository.GetGeocodeAsync(city, state, countryCode);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedGeocode, result);
        openWeatherApiMock.Verify(x => x.GetGeocodeAsync(city, state, countryCode), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task GetGeocodeAsync_ByCityStateCountry_WhenApiThrowsException_PropagatesException
    (
        [Frozen] Mock<IOpenWeatherApi> openWeatherApiMock,
        string city,
        string state,
        string countryCode
    )
    {
        //Arrange
        var expectedException = new Exception("API Error");
        openWeatherApiMock.Setup(x => x.GetGeocodeAsync(city, state, countryCode)).ThrowsAsync(expectedException);
        var repository = new GeocodeRepository(openWeatherApiMock.Object);

        //Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => repository.GetGeocodeAsync(city, state, countryCode));
        Assert.Equal(expectedException.Message, exception.Message);
        openWeatherApiMock.Verify(x => x.GetGeocodeAsync(city, state, countryCode), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task GetGeocodeAsyncByPostalCode_CallsOpenWeatherApiAndReturnsResult
    (
        [Frozen] Mock<IOpenWeatherApi> openWeatherApiMock,
        string postalCode,
        string countryCode,
        Geocode expectedGeocode
    )
    {
        //Arrange
        openWeatherApiMock.Setup(x => x.GetGeocodePostalCodeAsync(postalCode, countryCode)).ReturnsAsync(expectedGeocode);
        var repository = new GeocodeRepository(openWeatherApiMock.Object);

        //Act
        var result = await repository.GetGeocodeAsyncByPostalCode(postalCode, countryCode);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedGeocode, result);
        openWeatherApiMock.Verify(x => x.GetGeocodePostalCodeAsync(postalCode, countryCode), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task GetGeocodeAsyncByPostalCode_WhenApiThrowsException_PropagatesException
    (
        [Frozen] Mock<IOpenWeatherApi> openWeatherApiMock,
        string postalCode,
        string countryCode
    )
    {
        //Arrange
        var expectedException = new Exception("API Error");
        openWeatherApiMock.Setup(x => x.GetGeocodePostalCodeAsync(postalCode, countryCode)).ThrowsAsync(expectedException);
        var repository = new GeocodeRepository(openWeatherApiMock.Object);

        //Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => repository.GetGeocodeAsyncByPostalCode(postalCode, countryCode));
        Assert.Equal(expectedException.Message, exception.Message);
        openWeatherApiMock.Verify(x => x.GetGeocodePostalCodeAsync(postalCode, countryCode), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task ReverseGeocodeAsync_CallsOpenWeatherApiAndReturnsResult
    (
        [Frozen] Mock<IOpenWeatherApi> openWeatherApiMock,
        string latitude,
        string longitude,
        ReverseGeocode expectedReverseGeocode
    )
    {
        //Arrange
        openWeatherApiMock.Setup(x => x.ReverseGeocodeAsync(latitude, longitude)).ReturnsAsync(expectedReverseGeocode);
        var repository = new GeocodeRepository(openWeatherApiMock.Object);

        //Act
        var result = await repository.ReverseGeocodeAsync(latitude, longitude);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedReverseGeocode, result);
        openWeatherApiMock.Verify(x => x.ReverseGeocodeAsync(latitude, longitude), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task ReverseGeocodeAsync_WhenApiThrowsException_PropagatesException
    (
        [Frozen] Mock<IOpenWeatherApi> openWeatherApiMock,
        string latitude,
        string longitude
    )
    {
        //Arrange
        var expectedException = new Exception("API Error");
        openWeatherApiMock.Setup(x => x.ReverseGeocodeAsync(latitude, longitude)).ThrowsAsync(expectedException);
        var repository = new GeocodeRepository(openWeatherApiMock.Object);

        //Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => repository.ReverseGeocodeAsync(latitude, longitude));
        Assert.Equal(expectedException.Message, exception.Message);
        openWeatherApiMock.Verify(x => x.ReverseGeocodeAsync(latitude, longitude), Times.Once);
    }
}
