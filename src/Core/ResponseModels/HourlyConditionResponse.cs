namespace Core.ResponseModels;

public class HourlyConditionResponse
{
    public DateTimeOffset Time { get; set; }
    public decimal Temperature { get; set; }
    public decimal FeelsLike { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    public decimal DewPoint { get; set; }
    public int CloudPercentage { get; set; }
    public decimal WindSpeed { get; set; }
    public decimal WindGusts { get; set; }
    public string? Description { get; set; }
    public int DescriptionId { get; set; }
}