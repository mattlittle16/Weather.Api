using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Core.DTOs;

public record struct OpenWeatherResponse
{
    [JsonPropertyName("lat")]
    public decimal? Lat { get; set; }

    [JsonPropertyName("lon")]
    public decimal? Lon { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("timezone_offset")]
    public int? TimezoneOffset { get; set; }

    [JsonPropertyName("current")]
    public CurrentChild? Current { get; set; }

    [JsonPropertyName("minutely")]
    public List<MinutelyChild>? Minutely { get; set; }

    [JsonPropertyName("hourly")]
    public List<HourlyChild>? Hourly { get; set; }

    [JsonPropertyName("daily")]
    public List<DailyChild>? Daily { get; set; }


    public record struct CurrentChild
    {
        [JsonPropertyName("dt")]
        public int? Dt { get; set; }

        [JsonPropertyName("sunrise")]
        public int? Sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public int? Sunset { get; set; }

        [JsonPropertyName("temp")]
        public decimal? Temp { get; set; }

        [JsonPropertyName("feels_like")]
        public decimal? FeelsLike { get; set; }

        [JsonPropertyName("pressure")]
        public int? Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public int? Humidity { get; set; }

        [JsonPropertyName("dew_point")]
        public decimal? DewPoint { get; set; }

        [JsonPropertyName("uvi")]
        public decimal? Uvi { get; set; }

        [JsonPropertyName("clouds")]
        public int? Clouds { get; set; }

        [JsonPropertyName("visibility")]
        public int? Visibility { get; set; }

        [JsonPropertyName("wind_speed")]
        public decimal? WindSpeed { get; set; }

        [JsonPropertyName("wind_deg")]
        public int? WindDeg { get; set; }

        [JsonPropertyName("wind_gust")]
        public decimal? WindGust { get; set; }

        [JsonPropertyName("weather")]
        public List<WeatherDescriptionChild>? Weather { get; set; }
    }

    public record struct DailyChild
    {
        [JsonPropertyName("dt")]
        public long? Dt { get; set; }

        [JsonPropertyName("sunrise")]
        public long? Sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public long? Sunset { get; set; }

        [JsonPropertyName("moonrise")]
        public long? Moonrise { get; set; }

        [JsonPropertyName("moonset")]
        public long? Moonset { get; set; }

        [JsonPropertyName("moon_phase")]
        public decimal? MoonPhase { get; set; }

        [JsonPropertyName("summary")]
        public string? Summary { get; set; }

        [JsonPropertyName("temp")]
        public TempChild? Temp { get; set; }

        [JsonPropertyName("feels_like")]
        public FeelsLikeChild? FeelsLike { get; set; }

        [JsonPropertyName("pressure")]
        public int? Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public int? Humidity { get; set; }

        [JsonPropertyName("dew_point")]
        public decimal? DewPoint { get; set; }

        [JsonPropertyName("wind_speed")]
        public decimal? WindSpeed { get; set; }

        [JsonPropertyName("wind_deg")]
        public int? WindDeg { get; set; }

        [JsonPropertyName("wind_gust")]
        public decimal? WindGust { get; set; }

        [JsonPropertyName("weather")]
        public List<WeatherDescriptionChild>? Weather { get; set; }

        [JsonPropertyName("clouds")]
        public int? Clouds { get; set; }

        [JsonPropertyName("pop")]
        public decimal? Pop { get; set; }

        [JsonPropertyName("uvi")]
        public decimal? Uvi { get; set; }

        [JsonPropertyName("rain")]
        public decimal? Rain { get; set; }
    }

    public record struct HourlyChild
    {
        [JsonPropertyName("dt")]
        public long? Dt { get; set; }

        [JsonPropertyName("temp")]
        public decimal? Temp { get; set; }

        [JsonPropertyName("feels_like")]
        public decimal? FeelsLike { get; set; }

        [JsonPropertyName("pressure")]
        public int? Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public int? Humidity { get; set; }

        [JsonPropertyName("dew_point")]
        public decimal? DewPoint { get; set; }

        [JsonPropertyName("uvi")]
        public decimal? Uvi { get; set; }

        [JsonPropertyName("clouds")]
        public int? Clouds { get; set; }

        [JsonPropertyName("visibility")]
        public int? Visibility { get; set; }

        [JsonPropertyName("wind_speed")]
        public decimal? WindSpeed { get; set; }

        [JsonPropertyName("wind_deg")]
        public int? WindDeg { get; set; }

        [JsonPropertyName("wind_gust")]
        public decimal? WindGust { get; set; }

        [JsonPropertyName("weather")]
        public List<WeatherDescriptionChild>? Weather { get; set; }

        [JsonPropertyName("pop")]
        public decimal? Pop { get; set; }

        [JsonPropertyName("rain")]
        public RainChild? Rain { get; set; }
    }

    public record struct MinutelyChild
    {
        [JsonPropertyName("dt")]
        public long? Dt { get; set; }

        [JsonPropertyName("precipitation")]
        public int? Precipitation { get; set; }
    }

    public record struct WeatherDescriptionChild
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("main")]
        public string? Main { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("icon")]
        public string? Icon { get; set; }
    }

    public record struct RainChild
    {
        [JsonPropertyName("1h")]
        public decimal? _1h { get; set; }
    }

    public record struct TempChild
    {
        [JsonPropertyName("day")]
        public decimal? Day { get; set; }

        [JsonPropertyName("min")]
        public decimal? Min { get; set; }

        [JsonPropertyName("max")]
        public decimal? Max { get; set; }

        [JsonPropertyName("night")]
        public decimal? Night { get; set; }

        [JsonPropertyName("eve")]
        public decimal? Eve { get; set; }

        [JsonPropertyName("morn")]
        public decimal? Morn { get; set; }
    }

    public record struct FeelsLikeChild
    {
        [JsonPropertyName("day")]
        public decimal? Day { get; set; }

        [JsonPropertyName("night")]
        public decimal? Night { get; set; }

        [JsonPropertyName("eve")]
        public decimal? Eve { get; set; }

        [JsonPropertyName("morn")]
        public decimal? Morn { get; set; }
    }

};