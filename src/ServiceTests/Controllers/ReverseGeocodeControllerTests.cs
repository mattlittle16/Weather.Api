using Xunit;
using FluentAssertions;

namespace ServiceTests.Controllers
{
    public class ReverseGeocodeControllerTests(IHttpClientFactory httpClientFactory)
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Constants.TestClient);

        [Fact]
        public async Task ReverseGeocode_Should_Return_Ok()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"reversegeocode?lat=34.2073&lon=-84.1402");
            var response = await _client.SendAsync(request);
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}