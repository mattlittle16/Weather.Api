using Core.Models;

namespace Core.Interfaces;

public interface IWeatherService 
{
    Task<WeatherRoot> GetWeatherAsync(string latitude, string longitude);

    Task<Geocode> GetGeocodeAsync(string city, string state, string postalCode);
}