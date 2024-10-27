using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Logger;

public class DbLoggerProvider : ILoggerProvider
{
    public readonly string _ConString; 

    public DbLoggerProvider(IConfiguration configuration)
    {
        _ConString = configuration.GetConnectionString("WeatherDb");
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new DbLogger(this);
    }

    public void Dispose() { }
}