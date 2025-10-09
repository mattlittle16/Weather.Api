using WireMock.Logging;
using WireMock.Settings;
using WireMock.Server;
using Wiremock;

var server = WireMockServer.Start(new WireMockServerSettings
{
    Port = 8080,
    Logger = new WireMockConsoleLogger()
});

var mocks = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(assembly => assembly.GetTypes())
    .Where(t => typeof(IMock).IsAssignableFrom(t) && !t.IsInterface);

foreach (var mock in mocks)
{
    var mockConfig = (IMock)Activator.CreateInstance(mock)!;
    mockConfig.RegisterMocks(server);
}

while (true)
{
    Console.WriteLine($"{DateTime.UtcNow} WireMock.Net server running");
    Thread.Sleep(30000);
}