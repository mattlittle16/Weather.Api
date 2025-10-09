using Core.DTOs;
using Core.Extensions;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Core.ApplicationLogic;

public class WeatherService(IWeatherRepository weatherRepository) : IWeatherService
{
    private readonly IWeatherRepository _weatherRepository = weatherRepository;

    public async Task<OpenWeatherResponse> GetWeatherAsync(string latitude, string longitude)
    {
        return await _weatherRepository.GetWeatherAsync(latitude, longitude);
    }
}