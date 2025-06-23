using Core.Models;

namespace Core.Interfaces;

public interface IOpenWeatherApi 
{
    Task<Geocode> GetGeocodeAsync(string city, string state, string postalCode);

    Task<WeatherRoot> GetWeatherInfoAsync(string Latitude, string Longitude);
}