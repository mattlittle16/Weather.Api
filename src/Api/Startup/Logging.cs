using Serilog;

namespace Api.Startup;

public static class Logging
{
    public static IServiceCollection AddCustomLogging(this IServiceCollection services)
    {
        // Serilog is configured in Program.cs via Host.UseSerilog()
        // Additional logging-related services can be added here if needed
        
        return services;
    }
}