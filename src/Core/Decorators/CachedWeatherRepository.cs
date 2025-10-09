using System.ComponentModel;
using Core.DTOs;
using Core.Extensions;
using Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Core.Decorators;

public class CachedWeatherRepository(IWeatherRepository weatherRepository, IMemoryCache cache, ILogger<CachedWeatherRepository> logger) : IWeatherRepository
{
    private readonly IWeatherRepository _weatherRepository = weatherRepository;
    private readonly IMemoryCache _cache = cache;
    private readonly ILogger<CachedWeatherRepository> _logger = logger;

    public async Task<OpenWeatherResponse> GetWeatherAsync(string latitude, string longitude)
    {
        var key = $"weather_{latitude}_{longitude}".GetHashString();

        if (_cache.TryGetValue(key, out OpenWeatherResponse? cachedWeather) && cachedWeather != null)
        {
            _logger.LogInformation("Fetching weather from cache for {Latitude}, {Longitude}", latitude, longitude);
            return cachedWeather;
        }

        _logger.LogInformation("Fetching weather from repository for {Latitude}, {Longitude}", latitude, longitude);
        var weather = await _weatherRepository.GetWeatherAsync(latitude, longitude);

        _cache.Set(key, weather, TimeSpan.FromMinutes(10));
        return weather;
    }
}