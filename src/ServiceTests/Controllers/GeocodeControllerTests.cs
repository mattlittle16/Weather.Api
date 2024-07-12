using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Xunit;
using FluentAssertions;
using Microsoft.VisualBasic;

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