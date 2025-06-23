using Xunit;
using FluentAssertions;

namespace ServiceTests.Controllers
{
    public class GeocodeControllerTests(IHttpClientFactory httpClientFactory)
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Constants.TestClient);

        [Fact]
        public async Task Geocode_Should_Return_Ok()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"geocode?city=cumming&state=ga&postalCode=30040");
            var response = await _client.SendAsync(request);
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}