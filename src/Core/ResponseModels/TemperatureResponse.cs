namespace Core.ResponseModels;

public class TemperatureResponse
{
    public decimal Day { get; set; }
    public decimal? Min { get; set; }
    public decimal? Max { get; set; }
    public decimal? Night { get; set; }
    public decimal Eve { get; set; }
    public decimal Morn { get; set; }
}