using Core.Configuration;
using Core.Models;
using Microsoft.Extensions.Options;

namespace Api.Middleware;

public class ApiKeyMiddleware(RequestDelegate next, IOptions<EnvironmentSettings> configuration)
{
    private const string APIKEYNAME = "x-api-key";

    public async Task Invoke(HttpContext context)
    {
        var exceptionMessage = string.Empty;
        var apiKey = configuration.Value.XApiKey;

        if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            exceptionMessage = "API Key was not provided.";
            
        if (!apiKey.Equals(extractedApiKey))
            exceptionMessage = "API Key is not valid.";

        if (!string.IsNullOrWhiteSpace(exceptionMessage)) 
            throw new FriendlyException("Forbidden", 401, exceptionMessage);

        await next(context);
    }
}