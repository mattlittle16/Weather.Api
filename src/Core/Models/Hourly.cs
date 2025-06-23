using System.Text.Json.Serialization;

namespace Core.Models;

public class Hourly
{
    [JsonPropertyName("dt")]
    public long? Dt { get; set; }

    [JsonPropertyName("temp")]
    public decimal? Temp { get; set; }

    [JsonPropertyName("feels_like")]
    public decimal? FeelsLike { get; set; }

    [JsonPropertyName("pressure")]
    public int? Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public int? Humidity { get; set; }

    [JsonPropertyName("dew_point")]
    public decimal? DewPoint { get; set; }

    [JsonPropertyName("uvi")]
    public decimal? Uvi { get; set; }

    [JsonPropertyName("clouds")]
    public int? Clouds { get; set; }

    [JsonPropertyName("visibility")]
    public int? Visibility { get; set; }

    [JsonPropertyName("wind_speed")]
    public decimal? WindSpeed { get; set; }

    [JsonPropertyName("wind_deg")]
    public int? WindDeg { get; set; }

    [JsonPropertyName("wind_gust")]
    public decimal? WindGust { get; set; }

    [JsonPropertyName("weather")]
    public List<WeatherDescription>? Weather { get; set; }

    [JsonPropertyName("pop")]
    public decimal? Pop { get; set; }

    [JsonPropertyName("rain")]
    public Rain? Rain { get; set; }
}