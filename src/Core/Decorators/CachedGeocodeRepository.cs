using Core.Extensions;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Core.Decorators;

public class CachedGeocodeRepository : IGeocodeRepository
{
    private readonly IGeocodeRepository _geocodeRepository;
    private readonly IMemoryCache _cache;
    private readonly ILogger<CachedGeocodeRepository> _logger;

    public CachedGeocodeRepository(
        IGeocodeRepository geocodeRepository,
        IMemoryCache cache,
        ILogger<CachedGeocodeRepository> logger)
    {
        _geocodeRepository = geocodeRepository;
        _cache = cache;
        _logger = logger;
    }

    public async Task<Geocode> GetGeocodeAsync(string city, string state, string countryCode)
    {
        var key = $"geocode_{city}_{state}_{countryCode}".GetHashString();

        if (_cache.TryGetValue(key, out Geocode? cachedGeocode) && cachedGeocode != null)
        {
            _logger.LogInformation("Fetching geocode from cache");
            return cachedGeocode;
        }

        _logger.LogInformation("Fetching geocode from repository");
        var geocode = await _geocodeRepository.GetGeocodeAsync(city, state, countryCode);

        _cache.Set(key, geocode, TimeSpan.FromHours(24));
        return geocode;
    }

    public async Task<Geocode> GetGeocodeAsyncByPostalCode(string postalCode, string countryCode)
    {
        var key = $"geocode_zip_{postalCode}_{countryCode}".GetHashString();

        if (_cache.TryGetValue(key, out Geocode? cachedGeocode) && cachedGeocode != null)
        {
            _logger.LogInformation("Fetching geocode by zip from cache");
            return cachedGeocode;
        }

        _logger.LogInformation("Fetching geocode by zip from repository");
        var geocode = await _geocodeRepository.GetGeocodeAsyncByPostalCode(postalCode, countryCode);

        _cache.Set(key, geocode, TimeSpan.FromHours(24));
        return geocode;
    }

    public async Task<ReverseGeocode> ReverseGeocodeAsync(string latitude, string longitude)
    {
        var key = $"reverse_geocode_{latitude}_{longitude}".GetHashString();

        if (_cache.TryGetValue(key, out ReverseGeocode? cachedReverseGeocode) && cachedReverseGeocode != null)
        {
            _logger.LogInformation("Fetching reverse geocode from cache");
            return cachedReverseGeocode;
        }

        _logger.LogInformation("Fetching reverse geocode from repository");
        var reverseGeocode = await _geocodeRepository.ReverseGeocodeAsync(latitude, longitude);

        _cache.Set(key, reverseGeocode, TimeSpan.FromHours(24));
        return reverseGeocode;
    }
}