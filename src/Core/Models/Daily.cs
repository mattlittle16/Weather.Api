using System.Text.Json.Serialization;

namespace Core.Models;

public class Daily
{
    [JsonPropertyName("dt")]
    public long? Dt { get; set; }

    [JsonPropertyName("sunrise")]
    public long? Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    public long? Sunset { get; set; }

    [JsonPropertyName("moonrise")]
    public long? Moonrise { get; set; }

    [JsonPropertyName("moonset")]
    public long? Moonset { get; set; }

    [JsonPropertyName("moon_phase")]
    public decimal? MoonPhase { get; set; }

    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    [JsonPropertyName("temp")]
    public Temp? Temp { get; set; }

    [JsonPropertyName("feels_like")]
    public FeelsLike? FeelsLike { get; set; }

    [JsonPropertyName("pressure")]
    public int? Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public int? Humidity { get; set; }

    [JsonPropertyName("dew_point")]
    public decimal? DewPoint { get; set; }

    [JsonPropertyName("wind_speed")]
    public decimal? WindSpeed { get; set; }

    [JsonPropertyName("wind_deg")]
    public int? WindDeg { get; set; }

    [JsonPropertyName("wind_gust")]
    public decimal? WindGust { get; set; }

    [JsonPropertyName("weather")]
    public List<WeatherDescription>? Weather { get; set; }

    [JsonPropertyName("clouds")]
    public int? Clouds { get; set; }

    [JsonPropertyName("pop")]
    public decimal? Pop { get; set; }

    [JsonPropertyName("uvi")]
    public decimal? Uvi { get; set; }

    [JsonPropertyName("rain")]
    public decimal? Rain { get; set; }
}