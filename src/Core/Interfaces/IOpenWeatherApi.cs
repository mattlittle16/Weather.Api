using Core.Models;

namespace Core.Interfaces;

public interface IOpenWeatherApi 
{
    Task<Geocode> GetGeocodeAsync(string city, string state, string countryCode);

    Task<Geocode> GetGeocodeZipAsync(string postalCode, string countryCode);

    Task<ReverseGeocode> ReverseGeocodeAsync(string latitude, string longitude);

    Task<WeatherRoot> GetWeatherInfoAsync(string Latitude, string Longitude);
}