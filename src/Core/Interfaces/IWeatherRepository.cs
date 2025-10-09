using Core.DTOs;

namespace Core.Interfaces;

public interface IWeatherRepository
{
    Task<OpenWeatherResponse> GetWeatherAsync(string latitude, string longitude);
}