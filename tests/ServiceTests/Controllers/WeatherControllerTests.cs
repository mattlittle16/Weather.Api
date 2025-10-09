using FluentAssertions;
using Xunit;

namespace ServiceTests.Controllers
{
    public class WeatherControllerTests(IHttpClientFactory httpClientFactory)
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Constants.TestClient);

        [Fact]
        public async Task Get_Should_Return_Ok()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"weather?lat=0&lon=0");
            var response = await _client.SendAsync(request);
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}