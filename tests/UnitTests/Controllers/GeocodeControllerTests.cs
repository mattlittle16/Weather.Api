using System.ComponentModel.DataAnnotations;
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

public class GeocodeControllerTests
{
    [Theory]
    [AutoMoqData]
    public async Task GetByPostalCode_ReturnsValidGeocode
    (
        [Frozen] Mock<IGeocodeService> geocodeServiceMock,
        [Frozen] Mock<ILogger<GeocodeController>> loggerMock,
        [MaxLength(2)]
        string countryCode,
        [MaxLength(5)]
        string postalCode,
        Geocode serviceResponse
    )
    {
        //Arrange 
        var validator = new GeocodeRequestValidator();
        geocodeServiceMock.Setup(x => x.GetGeocodeAsync(postalCode, countryCode)).ReturnsAsync(serviceResponse);
        var controller = new GeocodeController(loggerMock.Object, geocodeServiceMock.Object, validator);

        //Act 
        var response = await controller.Get(new GeocodeRequestModel(null, null, postalCode, countryCode));

        //Assert
        var result = response;

        Assert.NotNull(result);
        Assert.True(((OkObjectResult)result).StatusCode == 200);
    }

    [Theory]
    [AutoMoqData]
    public async Task GetByCityState_ReturnsValidGeocode
    (
        [Frozen] Mock<IGeocodeService> geocodeServiceMock,
        [Frozen] Mock<ILogger<GeocodeController>> loggerMock,
        [MaxLength(2)]
        string state,
        string city,
        [MaxLength(2)]
        string countryCode,
        Geocode serviceResponse
    )
    {
        //Arrange 
        var validator = new GeocodeRequestValidator();
        geocodeServiceMock.Setup(x => x.GetGeocodeAsync(city, state, countryCode)).ReturnsAsync(serviceResponse);
        var controller = new GeocodeController(loggerMock.Object, geocodeServiceMock.Object, validator);

        //Act 
        var response = await controller.Get(new GeocodeRequestModel(city, state, null, countryCode));

        //Assert
        var result = response;

        Assert.NotNull(result);
        Assert.True(((OkObjectResult)result).StatusCode == 200);
    }
}