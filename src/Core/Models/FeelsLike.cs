using System.Text.Json.Serialization;

namespace Core.Models;

public class FeelsLike
{
    [JsonPropertyName("day")]
    public double? Day { get; set; }

    [JsonPropertyName("night")]
    public double? Night { get; set; }

    [JsonPropertyName("eve")]
    public double? Eve { get; set; }

    [JsonPropertyName("morn")]
    public double? Morn { get; set; }
}