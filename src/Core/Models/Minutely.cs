using System.Text.Json.Serialization;

namespace Core.Models;

public class Minutely
{
    [JsonPropertyName("dt")]
    public long? Dt { get; set; }

    [JsonPropertyName("precipitation")]
    public int? Precipitation { get; set; }
}