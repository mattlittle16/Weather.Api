using Core.Models;

namespace Core.Interfaces;

public interface IGeocodeRepository
{
    Task<Geocode> GetGeocodeAsync(string city, string state, string countryCode);
    Task<Geocode> GetGeocodeAsyncByPostalCode(string postalCode, string countryCode);
    Task<ReverseGeocode> ReverseGeocodeAsync(string latitude, string longitude);
}