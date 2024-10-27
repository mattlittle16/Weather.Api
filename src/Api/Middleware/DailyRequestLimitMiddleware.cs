using System.Text.Json;
using Core.Configuration;
using Core.ResponseModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Api.Middleware;

public class DailyRequestLimitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMemoryCache _cache;
    private readonly EnvironmentSettings _settings;

    public DailyRequestLimitMiddleware(IMemoryCache cache, RequestDelegate next, IOptions<EnvironmentSettings> options)
    {
        _cache = cache;
        _next = next;
        _settings = options.Value;
    }

    public async Task Invoke(HttpContext context)
    {
        var cacheKey = DateTimeOffset.UtcNow.Date.ToString("MM-dd-yyyy")+"openweatherhits";
        if (_cache.TryGetValue(cacheKey, out int hits))
        {
            hits++;
            _cache.Set(cacheKey, hits, TimeSpan.FromHours(24));
        }
        else 
        {
            hits = 1;
            _cache.Set(cacheKey, hits, TimeSpan.FromHours(24));
        }

        if (hits == _settings.DailyOpenWeatherApiLimit) 
        {
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            context.Response.ContentType = "text/plain";   

            await context.Response.WriteAsync(JsonSerializer.Serialize(new ExceptionResponse { Message = "Daily request limit hit" }));
            return;
        }
        else 
        {
            await _next(context);
        }        
    }
}