using System.Net;
using WireMock;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Types;
using WireMock.Util;

namespace Wiremock.Mocks
{
    public class Geocode : IMock
    {
        public void RegisterMocks(WireMockServer server)
        {
            server.Given(Request
                .Create()
                .WithPath(new RegexMatcher("/geo/1.0/direct"))
                .UsingGet())

                .RespondWith(
                    Response
                    .Create()
                    .WithCallback(_ =>
                    {
                        return GetResponse();
                    })
                );
        }

        public ResponseMessage GetResponse()
        {
            var response = new ResponseMessage
            {
                BodyData = new BodyData
                {
                    BodyAsJson = new List<object> {
                        new {
                           name = "Cumming",
                           lat = -84.1402M,
                           lon = 34.2073M,
                           country = "us",
                           state = "Georgia"
                        }
                    },
                    DetectedBodyType = BodyType.Json
                },
                StatusCode = HttpStatusCode.OK
            };

            return response;
        }
    }
}