using Core.Interfaces;
using Core.Models;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class GeocodeRepository(IOpenWeatherApi openWeatherApi) : IGeocodeRepository
{
    private readonly IOpenWeatherApi _openWeatherApi = openWeatherApi;

    public async Task<Geocode> GetGeocodeAsync(string city, string state, string countryCode)
    {
        return await _openWeatherApi.GetGeocodeAsync(city, state, countryCode);
    }

    public async Task<Geocode> GetGeocodeAsyncByPostalCode(string postalCode, string countryCode)
    {
        return await _openWeatherApi.GetGeocodePostalCodeAsync(postalCode, countryCode);
    }

    public async Task<ReverseGeocode> ReverseGeocodeAsync(string latitude, string longitude)
    {
        return await _openWeatherApi.ReverseGeocodeAsync(latitude, longitude);
    }
}