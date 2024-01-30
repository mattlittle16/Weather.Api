using System.Text.Json;
using Core.Extensions;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Api.ApplicationLogic;

public class WeatherService : IWeatherService
{
    private readonly IOpenWeatherApi _openWeatherApi;
    private readonly IMemoryCache _cache;

    public WeatherService(IOpenWeatherApi openWeatherApi, IMemoryCache cache)
    {
        _openWeatherApi = openWeatherApi;
        _cache = cache;
    }

    public async Task<Geocode> GetGeocodeAsync(string city, string state, string postalCode)
    {
        var key = (city+state+postalCode).GetHashString();

        if (_cache.TryGetValue(key, out Geocode cachedGeocode))
        {
            return cachedGeocode;
        }

        var geocode = await _openWeatherApi.GetGeocodeAsync(city, state, postalCode);

        _cache.Set((city+state+postalCode).GetHashString(), geocode, TimeSpan.FromMinutes(1));

        return geocode;
    }

    public async Task<WeatherRoot> GetWeatherAsync(string latitude, string longitude)
    {
        var key = (latitude+longitude).GetHashString();

        if (_cache.TryGetValue(key, out WeatherRoot cachedWeather))
        {
            return cachedWeather;
        }

        var weather = await _openWeatherApi.GetWeatherInfoAsync(latitude, longitude);

        _cache.Set(key, weather, TimeSpan.FromMinutes(1));

        return weather;
    }
}