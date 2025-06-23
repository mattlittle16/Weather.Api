using System.Text.Json.Serialization;

namespace Core.Models;

public class Current
{
    [JsonPropertyName("dt")]
    public int? Dt { get; set; }

    [JsonPropertyName("sunrise")]
    public int? Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    public int? Sunset { get; set; }

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
}