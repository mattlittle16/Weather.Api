using Core.DTOs;
using Core.Models;

namespace Core.Interfaces;

public interface IWeatherService
{
    Task<OpenWeatherResponse> GetWeatherAsync(string latitude, string longitude);

    Task<Geocode> GetGeocodeAsync(string city, string state, string countryCode);

    Task<Geocode> GetGeocodeAsync(string postalCode, string countryCode);

    Task<ReverseGeocode> ReverseGeocodeAsync(string latitude, string longitude);
}