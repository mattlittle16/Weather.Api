namespace Core.Configuration;

public class EnvironmentSettings 
{
    public string? OpenWeatherApiKey { get; set; }

    public string? OpenWeatherApiBaseUrl { get; set; }

    public string XApiKey { get; set; } = string.Empty;

    public int RateLimit { get; set; } = 50;

    public int RateLimitTimeInMinutes { get; set; } = 1;

    public int DailyOpenWeatherApiLimit { get; set; } = 990;
}