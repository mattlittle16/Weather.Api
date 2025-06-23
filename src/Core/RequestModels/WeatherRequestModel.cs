namespace Core.RequestModels
{
    public record WeatherRequestModel
    {        
        public string? Lat { get; init; } 
        
        public string? Lon { get; init; }
    }
}