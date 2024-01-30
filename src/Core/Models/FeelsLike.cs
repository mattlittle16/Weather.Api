using System.Text.Json.Serialization;

namespace Core.Models;

public class FeelsLike
{
    [JsonPropertyName("day")]
    public decimal? Day { get; set; }

    [JsonPropertyName("night")]
    public decimal? Night { get; set; }

    [JsonPropertyName("eve")]
    public decimal? Eve { get; set; }

    [JsonPropertyName("morn")]
    public decimal? Morn { get; set; }
}