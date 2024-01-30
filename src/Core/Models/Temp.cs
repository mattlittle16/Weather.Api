using System.Text.Json.Serialization;

namespace Core.Models;

public class Temp
{
    [JsonPropertyName("day")]
    public decimal? Day { get; set; }

    [JsonPropertyName("min")]
    public decimal? Min { get; set; }

    [JsonPropertyName("max")]
    public decimal? Max { get; set; }

    [JsonPropertyName("night")]
    public decimal? Night { get; set; }

    [JsonPropertyName("eve")]
    public decimal? Eve { get; set; }

    [JsonPropertyName("morn")]
    public decimal? Morn { get; set; }
}