using Core.Models;

namespace Core.ResponseModels;

public class WeatherResponse
{    
    public WeatherResponse()
    {

    }

    public WeatherResponse(WeatherRoot root)
    {
        MapWeatherRootToModel(root);
    }

    public CurrentConditionResponse CurrentCondition { get; set; } = new CurrentConditionResponse();
    public List<HourlyConditionResponse> HourlyConditions { get; set; } = new List<HourlyConditionResponse>();
    public List<DailyConditionResponse> DailyConditions { get; set; } = new List<DailyConditionResponse>();

    private void MapWeatherRootToModel(WeatherRoot root)
    {
        //Current
        if (root.Current?.Weather != null && root.Current.Weather.FirstOrDefault() is { } currentWeatherDesc)
        {
            CurrentCondition.Description = currentWeatherDesc.Description;
            CurrentCondition.DescriptionId = currentWeatherDesc.Id ?? 0;
        }
        if (root.Current?.Pressure is not null)
            CurrentCondition.Pressure = root.Current.Pressure.Value;
        if (root.Current?.Temp is not null)
            CurrentCondition.Temperature = root.Current.Temp.Value;
        if (root.Current?.FeelsLike is not null)
            CurrentCondition.FeelsLike = root.Current.FeelsLike.Value;
        if (root.Current?.Clouds is not null)
            CurrentCondition.CloudPercentage = root.Current.Clouds.Value;
        if (root.Current?.WindGust is not null)
            CurrentCondition.WindGusts = root.Current.WindGust.Value;
        if (root.Current?.WindSpeed is not null)
            CurrentCondition.WindSpeed = root.Current.WindSpeed.Value;
        if (root.Current?.Uvi is not null)
            CurrentCondition.UVIndex = root.Current.Uvi.Value;

        //Hourly
        HourlyConditions = root.Hourly?.Select(x => {
            var weatherDesc = x.Weather?.FirstOrDefault();
            return new HourlyConditionResponse 
            {
                Time = x.Dt is not null ? DateTimeOffset.FromUnixTimeSeconds(x.Dt.Value) : default,
                Temperature = x.Temp ?? default,
                FeelsLike = x.FeelsLike ?? default,
                Pressure = x.Pressure ?? default,
                Humidity = x.Humidity ?? default,
                DewPoint = x.DewPoint ?? default,
                CloudPercentage = x.Clouds ?? default,
                Description = weatherDesc?.Description,
                DescriptionId = weatherDesc?.Id ?? 0,
                WindGusts = x.WindGust ?? default,
                WindSpeed = x.WindSpeed ?? default
            };
        }).ToList() ?? new List<HourlyConditionResponse>();

        //Daily
        DailyConditions = root.Daily?.Select(x => {
            var weatherDesc = x.Weather?.FirstOrDefault();
            return new DailyConditionResponse
            {
                Time = x.Dt is not null ? DateTimeOffset.FromUnixTimeSeconds(x.Dt.Value) : default,
                Sunrise = x.Sunrise is not null ? DateTimeOffset.FromUnixTimeSeconds(x.Sunrise.Value) : default,
                Sunset = x.Sunset is not null ? DateTimeOffset.FromUnixTimeSeconds(x.Sunset.Value) : default,
                Summary = x.Summary,
                Temp = x.Temp is not null ? new TemperatureResponse
                {
                    Day = x.Temp.Day ?? default,
                    Min = x.Temp.Min ?? default,
                    Max = x.Temp.Max ?? default,
                    Night = x.Temp.Night ?? default,
                    Eve = x.Temp.Eve ?? default,
                    Morn = x.Temp.Morn ?? default    
                } : null, 
                FeelsLike = x.FeelsLike is not null ? new TemperatureResponse 
                {
                    Day = x.FeelsLike.Day ?? default,
                    Night = x.FeelsLike.Night ?? default,
                    Eve = x.FeelsLike.Eve ?? default,
                    Morn = x.FeelsLike.Morn ?? default    
                } : null,
                Pressure = x.Pressure ?? default,
                Humidity = x.Humidity ?? default,
                WindSpeed = x.WindSpeed ?? default,
                WindGusts = x.WindGust ?? default,
                Description = weatherDesc?.Description,
                DescriptionId = weatherDesc?.Id ?? 0,
                CloudPercentage = x.Clouds ?? default,
                UVIndex = x.Uvi ?? default        
            };
        }).ToList() ?? new List<DailyConditionResponse>();
    }
}