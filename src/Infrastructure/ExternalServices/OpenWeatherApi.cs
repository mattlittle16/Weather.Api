using System.Net.Sockets;
using System.Text.Json;
using System.Web;
using Core.Configuration;
using Core.Constants;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.ExternalServices;

public class OpenWeatherApi : IOpenWeatherApi
{
    private EnvironmentSettings _environmentSettings { get; set; } 
    private IOpenWeatherApiRefit _client { get; set; }
    private readonly ILogger<OpenWeatherApi> _logger;

    public OpenWeatherApi(IOptions<EnvironmentSettings> options, IOpenWeatherApiRefit client, ILogger<OpenWeatherApi> logger)
    {
        _environmentSettings = options.Value;
        _client = client;
        _logger = logger;
    }

    public async Task<Geocode> GetGeocodeAsync(string city, string state, string postalCode)
    {        
        var response = await _client.GetGeocodeAsync(city, state, postalCode, _environmentSettings.OpenWeatherApiKey);
        
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
        var response = await _client.GetWeatherAsync(latitude, longitude, _environmentSettings.OpenWeatherApiKey);

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