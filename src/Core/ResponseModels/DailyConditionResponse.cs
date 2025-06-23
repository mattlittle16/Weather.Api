namespace Core.ResponseModels;

public class DailyConditionResponse
{
    public DateTimeOffset Time { get; set; }
    public DateTimeOffset Sunrise { get; set; }
    public DateTimeOffset Sunset { get; set; }
    public string? Summary { get; set; }

    public TemperatureResponse? Temp { get; set; } 

    public TemperatureResponse? FeelsLike { get; set; }

    public int Pressure { get; set; }

    public int Humidity { get; set; }

    public decimal WindSpeed { get; set; }

    public decimal WindGusts { get; set; }

    public string? Description { get; set; }

    public int DescriptionId { get; set; }

    public int CloudPercentage { get; set; }

    public decimal UVIndex { get; set; }

}