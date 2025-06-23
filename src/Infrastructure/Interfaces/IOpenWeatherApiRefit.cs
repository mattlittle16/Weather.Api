using Refit;

namespace Infrastructure.Interfaces
{
    public interface IOpenWeatherApiRefit
    {
        [Get("/geo/1.0/direct?q={city},{state},{postalCode}&limit=1&appid={openWeatherApiKey}")]
        Task<HttpResponseMessage> GetGeocodeAsync(string city, string state, string postalCode, string? openWeatherApiKey);

        [Get("/data/3.0/onecall?lat={latitude}&lon={longitude}&units=imperial&appid={openWeatherApiKey}")]
        Task<HttpResponseMessage> GetWeatherAsync(string latitude, string longitude, string? openWeatherApiKey);
    }
}