using System.Text.Json.Serialization;


namespace Core.Models;

public class WeatherRoot
{
    [JsonPropertyName("lat")]
    public decimal? Lat { get; set; }

    [JsonPropertyName("lon")]
    public decimal? Lon { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("timezone_offset")]
    public int? TimezoneOffset { get; set; }

    [JsonPropertyName("current")]
    public Current? Current { get; set; }

    [JsonPropertyName("minutely")]
    public List<Minutely>? Minutely { get; set; }

    [JsonPropertyName("hourly")]
    public List<Hourly>? Hourly { get; set; }

    [JsonPropertyName("daily")]
    public List<Daily>? Daily { get; set; }
}