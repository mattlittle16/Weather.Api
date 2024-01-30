using System.Text.Json;
using System.Text.Json.Serialization;
using Core.Configuration;
using Core.Constants;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.ExternalServices;

public class OpenWeatherApi : IOpenWeatherApi
{
    private EnvironmentSettings _environmentSettings { get; set; } 
    private readonly HttpClient _httpClient;
    private readonly ILogger<OpenWeatherApi> _logger;

    public OpenWeatherApi(IOptions<EnvironmentSettings> options, IHttpClientFactory httpClientFactory, ILogger<OpenWeatherApi> logger)
    {
        _environmentSettings = options.Value;
        _httpClient = httpClientFactory.CreateClient(Constants.OpenWeatherApi);
        _logger = logger;
    }

    public async Task<Geocode> GetGeocodeAsync(string city, string state, string postalCode)
    {
        var url = $"geo/1.0/direct?q={city},{state},{postalCode}&limit=1&appid={_environmentSettings.OpenWeatherApiKey}";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var geocodeList = JsonSerializer.Deserialize<List<Geocode>>(await response.Content.ReadAsStringAsync());
            if (geocodeList.Count > 0) 
            {
                return geocodeList[0];
            }
            else
            {
                var message = $"get geocode call failed {response}";
                throw new Exception(message);
            }
        }
        else
        {
            _logger.LogError("geocoding call failed {response}", response.StatusCode);
            var message = $"get geocode call failed {response}";
            throw new Exception(message);
        }
    }

    public async Task<WeatherRoot> GetWeatherInfoAsync(string latitude, string longitude)
    {
        var url = $"data/3.0/onecall?lat={latitude}&lon={longitude}&units=imperial&appid={_environmentSettings.OpenWeatherApiKey}";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<WeatherRoot>(await response.Content.ReadAsStringAsync());
        }
        else
        {
            _logger.LogError("geocoding call failed {response}", response.StatusCode);
            var message = $"get weather call failed {response}";
            throw new Exception(message);
        }        
    }
}