using System.Text.Json.Serialization;

namespace Core.Models;

public class Rain
{
    [JsonPropertyName("1h")]
    public double? _1h { get; set; }
}