using System.Text.Json.Serialization;

namespace Core.Models;

public record ReverseGeocode
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("lat")]
    public decimal Lat { get; init; }

    [JsonPropertyName("lon")]
    public decimal Lon { get; init; }

    [JsonPropertyName("country")]
    public string? Country { get; init; }

    [JsonPropertyName("state")]
    public string? State { get; init; }

    [JsonPropertyName("local_names")]
    public LocalName? LocalNames { get; init; }

    public record LocalName
    {
        [JsonPropertyName("en")]
        public string? En { get; init; }
    }
}