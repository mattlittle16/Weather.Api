namespace Core.Configuration;

public class EnvironmentSettings 
{
    public string OpenWeatherApiKey { get; set; }

    public int RateLimit { get; set; } = 10;

    public int RateLimitTimeInMinutes { get; set; } = 1;
}