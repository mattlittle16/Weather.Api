using Core.DTOs;
using Core.Interfaces;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class WeatherRepository(IOpenWeatherApi openWeatherApi) : IWeatherRepository
{
    private readonly IOpenWeatherApi _openWeatherApi = openWeatherApi;

    public async Task<OpenWeatherResponse> GetWeatherAsync(string latitude, string longitude)
    {
        return await _openWeatherApi.GetWeatherInfoAsync(latitude, longitude);
    }
}