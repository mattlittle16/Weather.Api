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
        CurrentCondition.Description = root.Current.Weather.First().Description;
        CurrentCondition.DescriptionId = root.Current.Weather.First().Id!.Value;
        CurrentCondition.Pressure = root.Current.Pressure!.Value;
        CurrentCondition.Temperature = root.Current.Temp!.Value;
        CurrentCondition.FeelsLike = root.Current.FeelsLike!.Value;
        CurrentCondition.CloudPercentage = root.Current.Clouds!.Value;
        CurrentCondition.WindGusts = root.Current.WindGust!.Value;
        CurrentCondition.WindSpeed = root.Current.WindSpeed!.Value;
        CurrentCondition.UVIndex = root.Current.Uvi!.Value;

        //Hourly
        HourlyConditions = root.Hourly.Select(x => {
            return new HourlyConditionResponse 
            {
                Time = DateTimeOffset.FromUnixTimeSeconds(x.Dt!.Value), 
                Temperature = x.Temp!.Value,
                FeelsLike = x.FeelsLike!.Value, 
                Pressure = x.Pressure!.Value,
                Humidity = x.Humidity!.Value, 
                DewPoint = x.DewPoint!.Value,
                CloudPercentage = x.Clouds!.Value,  
                Description = x.Weather.First().Description,
                DescriptionId = x.Weather.First().Id!.Value, 
                WindGusts = x.WindGust!.Value,
                WindSpeed = x.WindSpeed!.Value 
            };
        }).ToList();

        //Daily
        DailyConditions = root.Daily.Select(x => {
            return new DailyConditionResponse
            {
                Time = DateTimeOffset.FromUnixTimeSeconds(x.Dt!.Value),
                Sunrise = DateTimeOffset.FromUnixTimeSeconds(x.Sunrise!.Value),  
                Sunset = DateTimeOffset.FromUnixTimeSeconds(x.Sunset!.Value),
                Summary = x.Summary,
                Temp = new TemperatureResponse
                {
                    Day = x.Temp.Day!.Value,
                    Min = x.Temp.Min!.Value,
                    Max = x.Temp.Max!.Value,
                    Night = x.Temp.Night!.Value,
                    Eve = x.Temp.Eve!.Value,
                    Morn = x.Temp.Morn!.Value    
                }, 
                FeelsLike = new TemperatureResponse 
                {
                    Day = x.FeelsLike.Day!.Value,
                    Night = x.FeelsLike.Night!.Value,
                    Eve = x.FeelsLike.Eve!.Value,
                    Morn = x.FeelsLike.Morn!.Value    
                },
                Pressure = x.Pressure!.Value,
                Humidity = x.Humidity!.Value,
                WindSpeed = x.WindSpeed!.Value,
                WindGusts = x.WindGust!.Value,
                Description = x.Weather.First().Description,
                DescriptionId = x.Weather.First().Id!.Value,
                CloudPercentage = x.Clouds!.Value,
                UVIndex = x.Uvi!.Value        
            };
        }).ToList();
    }
}