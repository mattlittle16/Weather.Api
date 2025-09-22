using Core.DTOs;
using Core.Extensions;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Api.ApplicationLogic;

public class WeatherService(IOpenWeatherApi openWeatherApi, IMemoryCache cache, ILogger<WeatherService> logger) : IWeatherService
{
    private readonly IOpenWeatherApi _openWeatherApi = openWeatherApi;
    private readonly IMemoryCache _cache = cache;

    public async Task<Geocode> GetGeocodeAsync(string city, string state, string countryCode)
    {
        var key = (city+state+countryCode).GetHashString();

        if (_cache.TryGetValue(key, out Geocode? cachedGeocode) && cachedGeocode is not null)
        {
            logger.LogInformation("Fetching geocode from cache");
            return (Geocode)cachedGeocode;
        }

        logger.LogInformation("Fetching geocode from OpenWeather API");
        var geocode = await _openWeatherApi.GetGeocodeAsync(city, state, countryCode);

        _cache.Set(key, geocode, TimeSpan.FromHours(24));

        return geocode;
    }
    
    public async Task<Geocode> GetGeocodeAsync(string postalCode, string countryCode)
    {
        var key = (postalCode+countryCode).GetHashString();

        if (_cache.TryGetValue(key, out Geocode? cachedGeocode) && cachedGeocode is not null)
        {
            logger.LogInformation("Fetching geocode from cache");
            return (Geocode)cachedGeocode;
        }

        logger.LogInformation("Fetching geocode from OpenWeather API");
        var geocode = await _openWeatherApi.GetGeocodeZipAsync(postalCode, countryCode);

        _cache.Set(key, geocode, TimeSpan.FromHours(24));

        return geocode;
    }

    public async Task<ReverseGeocode> ReverseGeocodeAsync(string latitude, string longitude)
    {
        var key = (latitude+longitude).GetHashString();

        if (_cache.TryGetValue(key, out ReverseGeocode? cachedReverseGeocode) && cachedReverseGeocode is not null)
        {
            logger.LogInformation("Fetching geocode from cache");
            return (ReverseGeocode)cachedReverseGeocode;
        }

        logger.LogInformation("Fetching geocode from OpenWeather API");
        var geocode = await _openWeatherApi.ReverseGeocodeAsync(latitude, longitude);

        _cache.Set(key, geocode, TimeSpan.FromHours(24));

        return geocode;
    }

    public async Task<OpenWeatherResponse> GetWeatherAsync(string latitude, string longitude)
    {
        var key = (latitude + longitude).GetHashString();

        if (_cache.TryGetValue(key, out OpenWeatherResponse? cachedWeather) && cachedWeather is not null)
        {
            logger.LogInformation("Fetching weather from cache");
            return (OpenWeatherResponse)cachedWeather;
        }

        logger.LogInformation("Fetching weather from OpenWeather API");
        var weather = await _openWeatherApi.GetWeatherInfoAsync(latitude, longitude);

        _cache.Set(key, weather, TimeSpan.FromMinutes(10));

        return weather;
    }
}