using Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Controllers;

public class HealthCheckControllerTests
{
    [Fact]
    public void Get_ReturnsOkResult()
    {
        //Arrange 
        var controller = new HealthCheckController();

        //Act 
        var response = controller.Get();

        //Assert
        Assert.NotNull(response);
        Assert.IsType<OkResult>(response);
        Assert.True(((OkResult)response).StatusCode == 200);
    }
}