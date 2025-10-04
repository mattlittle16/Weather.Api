using Core.DTOs;

namespace Core.Models;

public record WeatherResponse
{
    public WeatherResponse()
    {
        CurrentCondition = new CurrentConditionResponse();
        HourlyConditions = new List<HourlyConditionResponse>();
        DailyConditions = new List<DailyConditionResponse>();
    }

    public WeatherResponse(OpenWeatherResponse obj)
    {
        CurrentCondition = new CurrentConditionResponse();
        HourlyConditions = new List<HourlyConditionResponse>();
        DailyConditions = new List<DailyConditionResponse>();
        MapWeatherRootToModel(obj);
    }

    public CurrentConditionResponse CurrentCondition { get; set; }
    public List<HourlyConditionResponse> HourlyConditions { get; set; }
    public List<DailyConditionResponse> DailyConditions { get; set; } 

    private void MapWeatherRootToModel(OpenWeatherResponse root)
    {
        //Current
        var currentWeatherDesc = root.Current?.Weather?.FirstOrDefault();
        CurrentCondition = new CurrentConditionResponse
        {
            Description = currentWeatherDesc?.Description,
            DescriptionId = currentWeatherDesc?.Id ?? 0,
            Pressure = root.Current?.Pressure ?? 0,
            Temperature = root.Current?.Temp ?? 0,
            FeelsLike = root.Current?.FeelsLike ?? 0,
            CloudPercentage = root.Current?.Clouds ?? 0,
            WindGusts = root.Current?.WindGust ?? 0,
            WindSpeed = root.Current?.WindSpeed ?? 0,
            UVIndex = root.Current?.Uvi ?? 0,
            Humidity = root.Current?.Humidity ?? 0
        };

        //Hourly
        HourlyConditions = root.Hourly?.Select(x =>
        {
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
                WindSpeed = x.WindSpeed ?? default,
                RainChance = x.Pop ?? default
            };
        }).ToList() ?? new List<HourlyConditionResponse>();

        //Daily
        DailyConditions = root.Daily?.Select(x =>
        {
            var weatherDesc = x.Weather?.FirstOrDefault();
            return new DailyConditionResponse
            {
                Time = x.Dt is not null ? DateTimeOffset.FromUnixTimeSeconds(x.Dt.Value) : default,
                Sunrise = x.Sunrise is not null ? DateTimeOffset.FromUnixTimeSeconds(x.Sunrise.Value) : default,
                Sunset = x.Sunset is not null ? DateTimeOffset.FromUnixTimeSeconds(x.Sunset.Value) : default,
                Summary = x.Summary,
                Temp = x.Temp != null ? new TemperatureResponse
                {
                    Day = x.Temp.Day ?? default,
                    Min = x.Temp.Min,
                    Max = x.Temp.Max,
                    Night = x.Temp.Night,
                    Eve = x.Temp.Eve ?? default,
                    Morn = x.Temp.Morn ?? default
                } : null,
                FeelsLike = x.FeelsLike != null ? new TemperatureResponse
                {
                    Day = x.FeelsLike.Day ?? default,
                    Night = x.FeelsLike.Night,
                    Max = null,
                    Min = null,
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
                UVIndex = x.Uvi ?? default,
                RainChance = x.Pop ?? default
            };
        }).ToList() ?? new List<DailyConditionResponse>();
    }


    public record CurrentConditionResponse
    {
        public decimal Temperature { get; set; }

        public decimal FeelsLike { get; set; }

        public int Pressure { get; set; }

        public int Humidity { get; set; }

        public decimal WindSpeed { get; set; }

        public decimal WindGusts { get; set; }

        public string? Description { get; set; }

        public int DescriptionId { get; set; }

        public int CloudPercentage { get; set; }

        public decimal UVIndex { get; set; }
    }

    public record DailyConditionResponse
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
        public decimal RainChance { get; set; }

    }

    public record HourlyConditionResponse
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
        public decimal RainChance { get; set; }
    }

    public record TemperatureResponse
    {
        public decimal Day { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
        public decimal? Night { get; set; }
        public decimal Eve { get; set; }
        public decimal Morn { get; set; }
    }
}