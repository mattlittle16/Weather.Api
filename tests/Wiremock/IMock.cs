using WireMock.Server;

namespace Wiremock
{
    public interface IMock
    {
        void RegisterMocks(WireMockServer server);
    }
}