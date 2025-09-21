using System.Text.Json;
using Core.Configuration;
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

    public async Task<Geocode> GetGeocodeAsync(string city, string state, string countryCode)
    {
        var response = await _client.GetGeocodeAsync(city, state, countryCode, _environmentSettings.OpenWeatherApiKey!);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Geocode>>(content)![0];   
        }
        else
        {
            _logger.LogError("geocoding call failed {response}", response.StatusCode);
            var message = $"get geocode call failed {response}";
            throw new Exception(message);
        }
    }
    
    public async Task<Geocode> GetGeocodeZipAsync(string postalCode, string countryCode)
    {
        var response = await _client.GetGeocodeByZipAsync(postalCode, countryCode, _environmentSettings.OpenWeatherApiKey!);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Geocode>(content);
        }
        else
        {
            _logger.LogError("geocoding by zip call failed {response}", response.StatusCode);
            var message = $"get geocode by zip call failed {response}";
            throw new Exception(message);
        }
    }

    public async Task<ReverseGeocode> ReverseGeocodeAsync(string latitude, string longitude)
    {
        var response = await _client.ReverseGeocodeAsync(latitude, longitude, _environmentSettings.OpenWeatherApiKey!);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ReverseGeocode>>(content)![0];
        }
        else
        {
            _logger.LogError("geocoding by zip call failed {response}", response.StatusCode);
            var message = $"get geocode by zip call failed {response}";
            throw new Exception(message);
        }
    }

    public async Task<WeatherRoot> GetWeatherInfoAsync(string latitude, string longitude)
    {
        var response = await _client.GetWeatherAsync(latitude, longitude, _environmentSettings.OpenWeatherApiKey);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            if (content is null || string.IsNullOrWhiteSpace(content))
            {
                var message = $"get weather call failed with empty content {response}";
                throw new Exception(message);
            }

            return JsonSerializer.Deserialize<WeatherRoot>(content)!;
        }
        else
        {
            _logger.LogError("geocoding call failed {response}", response.StatusCode);
            var message = $"get weather call failed {response}";
            throw new Exception(message);
        }
    }
}