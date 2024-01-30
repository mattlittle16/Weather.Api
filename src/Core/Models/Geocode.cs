using System.Text.Json.Serialization;

namespace Core.Models;

public class Geocode
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("lat")]
    public string Lat { get; set; }

    [JsonPropertyName("lon")]
    public string Lon { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }
    
    [JsonPropertyName("state")]
    public string State { get; set; }
}