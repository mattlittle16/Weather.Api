using Refit;

namespace Infrastructure.Interfaces
{
    public interface IOpenWeatherApiRefit
    {
        [Get("/geo/1.0/direct?q={city},{state},{countryCode}&limit=1&appid={openWeatherApiKey}")]
        Task<HttpResponseMessage> GetGeocodeAsync(string city, string state, string countryCode, string openWeatherApiKey);

        [Get("/geo/1.0/zip?zip={postalCode},{countryCode}&limit=1&appid={openWeatherApiKey}")]
        Task<HttpResponseMessage> GetGeocodeByPostalCodeAsync(string postalCode, string countryCode, string openWeatherApiKey);

        [Get("/geo/1.0/reverse?lat={latitude}&lon={longitude}&limit=1&appid={openWeatherApiKey}")]
        Task<HttpResponseMessage> ReverseGeocodeAsync(string latitude, string longitude, string openWeatherApiKey);

        [Get("/data/3.0/onecall?lat={latitude}&lon={longitude}&units=imperial&appid={openWeatherApiKey}")]
        Task<HttpResponseMessage> GetWeatherAsync(string latitude, string longitude, string? openWeatherApiKey);
    }
}