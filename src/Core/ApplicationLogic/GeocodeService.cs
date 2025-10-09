using Core.DTOs;
using Core.Extensions;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Core.ApplicationLogic;

public class GeocodeService(IGeocodeRepository geocodeRepository) : IGeocodeService
{
    private readonly IGeocodeRepository _geocodeRepository = geocodeRepository;

    public async Task<Geocode> GetGeocodeAsync(string city, string state, string countryCode)
    {
        return await _geocodeRepository.GetGeocodeAsync(city, state, countryCode);
    }

    public async Task<Geocode> GetGeocodeAsync(string postalCode, string countryCode)
    {
        return await _geocodeRepository.GetGeocodeAsyncByPostalCode(postalCode, countryCode);
    }

    public async Task<ReverseGeocode> ReverseGeocodeAsync(string latitude, string longitude)
    {
        return await _geocodeRepository.ReverseGeocodeAsync(latitude, longitude);
    }
}