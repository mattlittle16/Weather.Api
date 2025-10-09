using System.ComponentModel.DataAnnotations;

namespace Core.RequestModels;

public record WeatherRequestModel(string? Lat, string? Lon);
