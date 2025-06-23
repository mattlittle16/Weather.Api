using Api.ApplicationLogic;
using AutoFixture.Xunit2;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Core.Extensions;

namespace UnitTests.Services
{
    public class OpenWeatherServiceTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Get_ReturnsValidGeocode_NoCache
        (
            [Frozen] Mock<IOpenWeatherApi> apiMock,
            string city, 
            string state, 
            string postalCode,
            Geocode returnValue 
        )
        {
            //arrange 
            apiMock.Setup(x => x.GetGeocodeAsync(city, state, postalCode)).ReturnsAsync(returnValue);
            var cacheMock = new MemoryCache(new MemoryCacheOptions());

            var loggerMock = new Mock<Microsoft.Extensions.Logging.ILogger<WeatherService>>();
            var service = new WeatherService(apiMock.Object, cacheMock, loggerMock.Object);

            //act
            var result = await service.GetGeocodeAsync(city, state, postalCode);

            //assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public async Task Get_ReturnsValidGeocode_WithCache
        (
            [Frozen] Mock<IOpenWeatherApi> apiMock,
            string city, 
            string state, 
            string postalCode,
            Geocode returnValue 
        )
        {
            //arrange 
            var cacheMock = new MemoryCache(new MemoryCacheOptions());
            cacheMock.Set((city+state+postalCode).GetHashString(), returnValue, TimeSpan.FromHours(24));

            var loggerMock = new Mock<Microsoft.Extensions.Logging.ILogger<WeatherService>>();
            var service = new WeatherService(apiMock.Object, cacheMock, loggerMock.Object);

            //act
            var result = await service.GetGeocodeAsync(city, state, postalCode);

            //assert
            Assert.NotNull(result);
        }
    }
}