using System.Text.Json;
using System.Threading.RateLimiting;
using Core.Configuration;
using Core.Constants;
using Core.ResponseModels;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

namespace Api.Configuration;
public class HostRateLimiterPolicy : IRateLimiterPolicy<string>
{
    private EnvironmentSettings _environmentSettings;

    public HostRateLimiterPolicy(IOptions<EnvironmentSettings> options)
    {
        _environmentSettings = options.Value;
    }

    public RateLimitPartition<string> GetPartition(HttpContext httpContext)
    {
        return RateLimitPartition.GetFixedWindowLimiter(httpContext.Request.Headers.Host.ToString(),
            partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = _environmentSettings.RateLimit,
                Window = TimeSpan.FromMinutes(_environmentSettings.RateLimitTimeInMinutes),
            });
    }

    public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected { get; } =
    (context, _) =>
    {
        context.HttpContext.Response.StatusCode = 429;
        context.HttpContext.Response.ContentType = "text/plain";
        context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(new ExceptionResponse { Message = "Too many requests" }));

        return new ValueTask();
    };
}

public static class ConfigureRateLimit
{
    public static void AddRateLimit(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.AddPolicy<string, HostRateLimiterPolicy>(Constants.RateLimitPolicy);
        });
    }
}