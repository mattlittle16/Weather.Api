using System.Net;
using System.Text.Json;
using AutoFixture;
using Core.Models;
using WireMock;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Types;
using WireMock.Util;


namespace Wiremock.Mocks
{
    public class Weather : IMock
    {
        public void RegisterMocks(WireMockServer server)
        {
            server.Given(Request
                .Create()
                .WithPath(new RegexMatcher("/data/3.0/onecall"))
                .UsingGet())
                
                .RespondWith(
                    Response
                    .Create()
                    .WithCallback(_ => {
                        return GetResponse();
                    })
                );
        }

        public ResponseMessage GetResponse() 
        {
            var fixture = new Fixture();
            var weather = fixture.Create<WeatherRoot>();

            var response = new ResponseMessage
            {
                BodyData = new BodyData
                {
                    BodyAsString = JsonSerializer.Serialize(weather),
                    DetectedBodyType = BodyType.String
                },
                StatusCode = HttpStatusCode.OK
            };

            return response;
        }
    }
}