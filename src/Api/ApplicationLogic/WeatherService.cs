using Core.Enums;
using Core.Extensions;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Api.ApplicationLogic;

public class WeatherService(IOpenWeatherApi openWeatherApi, IMemoryCache cache, ILogger<WeatherService> logger) : IWeatherService
{
    private readonly IOpenWeatherApi _openWeatherApi = openWeatherApi;
    private readonly IMemoryCache _cache = cache;

    public async Task<Geocode> GetGeocodeAsync(string city, string state, string postalCode)
    {
        var key = (city+state+postalCode).GetHashString();

        if (_cache.TryGetValue(key, out Geocode? cachedGeocode) && cachedGeocode is not null)
        {
            logger.LogInformation("Fetching geocode from cache");
            return cachedGeocode;
        }

        logger.LogInformation("Fetching geocode from OpenWeather API");
        var geocode = await _openWeatherApi.GetGeocodeAsync(city, state, postalCode);

        _cache.Set((city+state+postalCode).GetHashString(), geocode, TimeSpan.FromHours(24));

        return geocode;
    }

    public async Task<WeatherRoot> GetWeatherAsync(string latitude, string longitude)
    {
        var key = (latitude+longitude).GetHashString();

        if (_cache.TryGetValue(key, out WeatherRoot? cachedWeather) && cachedWeather is not null)
        {
            logger.LogInformation("Fetching weather from cache");
            return cachedWeather;
        }

        logger.LogInformation("Fetching weather from OpenWeather API");
        var weather = await _openWeatherApi.GetWeatherInfoAsync(latitude, longitude);

        _cache.Set(key, weather, TimeSpan.FromMinutes(10));

        return weather;
    }
}