using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Core.DTOs;

public record OpenWeatherResponse
{
    [JsonPropertyName("lat")]
    public decimal? Lat { get; init; }

    [JsonPropertyName("lon")]
    public decimal? Lon { get; init; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; init; }

    [JsonPropertyName("timezone_offset")]
    public int? TimezoneOffset { get; init; }

    [JsonPropertyName("current")]
    public CurrentChild? Current { get; init; }

    [JsonPropertyName("minutely")]
    public List<MinutelyChild>? Minutely { get; init; }

    [JsonPropertyName("hourly")]
    public List<HourlyChild>? Hourly { get; init; }

    [JsonPropertyName("daily")]
    public List<DailyChild>? Daily { get; init; }


    public record CurrentChild
    {
        [JsonPropertyName("dt")]
        public int? Dt { get; init; }

        [JsonPropertyName("sunrise")]
        public int? Sunrise { get; init; }

        [JsonPropertyName("sunset")]
        public int? Sunset { get; init; }

        [JsonPropertyName("temp")]
        public decimal? Temp { get; init; }

        [JsonPropertyName("feels_like")]
        public decimal? FeelsLike { get; init; }

        [JsonPropertyName("pressure")]
        public int? Pressure { get; init; }

        [JsonPropertyName("humidity")]
        public int? Humidity { get; init; }

        [JsonPropertyName("dew_point")]
        public decimal? DewPoint { get; init; }

        [JsonPropertyName("uvi")]
        public decimal? Uvi { get; init; }

        [JsonPropertyName("clouds")]
        public int? Clouds { get; init; }

        [JsonPropertyName("visibility")]
        public int? Visibility { get; init; }

        [JsonPropertyName("wind_speed")]
        public decimal? WindSpeed { get; init; }

        [JsonPropertyName("wind_deg")]
        public int? WindDeg { get; init; }

        [JsonPropertyName("wind_gust")]
        public decimal? WindGust { get; init; }

        [JsonPropertyName("weather")]
        public List<WeatherDescriptionChild>? Weather { get; init; }
    }

    public record DailyChild
    {
        [JsonPropertyName("dt")]
        public long? Dt { get; init; }

        [JsonPropertyName("sunrise")]
        public long? Sunrise { get; init; }

        [JsonPropertyName("sunset")]
        public long? Sunset { get; init; }

        [JsonPropertyName("moonrise")]
        public long? Moonrise { get; init; }

        [JsonPropertyName("moonset")]
        public long? Moonset { get; init; }

        [JsonPropertyName("moon_phase")]
        public decimal? MoonPhase { get; init; }

        [JsonPropertyName("summary")]
        public string? Summary { get; init; }

        [JsonPropertyName("temp")]
        public TempChild? Temp { get; init; }

        [JsonPropertyName("feels_like")]
        public FeelsLikeChild? FeelsLike { get; init; }

        [JsonPropertyName("pressure")]
        public int? Pressure { get; init; }

        [JsonPropertyName("humidity")]
        public int? Humidity { get; init; }

        [JsonPropertyName("dew_point")]
        public decimal? DewPoint { get; init; }

        [JsonPropertyName("wind_speed")]
        public decimal? WindSpeed { get; init; }

        [JsonPropertyName("wind_deg")]
        public int? WindDeg { get; init; }

        [JsonPropertyName("wind_gust")]
        public decimal? WindGust { get; init; }

        [JsonPropertyName("weather")]
        public List<WeatherDescriptionChild>? Weather { get; init; }

        [JsonPropertyName("clouds")]
        public int? Clouds { get; init; }

        [JsonPropertyName("pop")]
        public decimal? Pop { get; init; }

        [JsonPropertyName("uvi")]
        public decimal? Uvi { get; init; }

        [JsonPropertyName("rain")]
        public decimal? Rain { get; init; }
    }

    public record HourlyChild
    {
        [JsonPropertyName("dt")]
        public long? Dt { get; init; }

        [JsonPropertyName("temp")]
        public decimal? Temp { get; init; }

        [JsonPropertyName("feels_like")]
        public decimal? FeelsLike { get; init; }

        [JsonPropertyName("pressure")]
        public int? Pressure { get; init; }

        [JsonPropertyName("humidity")]
        public int? Humidity { get; init; }

        [JsonPropertyName("dew_point")]
        public decimal? DewPoint { get; init; }

        [JsonPropertyName("uvi")]
        public decimal? Uvi { get; init; }

        [JsonPropertyName("clouds")]
        public int? Clouds { get; init; }

        [JsonPropertyName("visibility")]
        public int? Visibility { get; init; }

        [JsonPropertyName("wind_speed")]
        public decimal? WindSpeed { get; init; }

        [JsonPropertyName("wind_deg")]
        public int? WindDeg { get; init; }

        [JsonPropertyName("wind_gust")]
        public decimal? WindGust { get; init; }

        [JsonPropertyName("weather")]
        public List<WeatherDescriptionChild>? Weather { get; init; }

        [JsonPropertyName("pop")]
        public decimal? Pop { get; init; }

        [JsonPropertyName("rain")]
        public RainChild? Rain { get; init; }
    }

    public record MinutelyChild
    {
        [JsonPropertyName("dt")]
        public long? Dt { get; init; }

        [JsonPropertyName("precipitation")]
        public int? Precipitation { get; init; }
    }

    public record WeatherDescriptionChild
    {
        [JsonPropertyName("id")]
        public int? Id { get; init; }

        [JsonPropertyName("main")]
        public string? Main { get; init; }

        [JsonPropertyName("description")]
        public string? Description { get; init; }

        [JsonPropertyName("icon")]
        public string? Icon { get; init; }
    }

    public record RainChild
    {
        [JsonPropertyName("1h")]
        public decimal? _1h { get; init; }
    }

    public record TempChild
    {
        [JsonPropertyName("day")]
        public decimal? Day { get; init; }

        [JsonPropertyName("min")]
        public decimal? Min { get; init; }

        [JsonPropertyName("max")]
        public decimal? Max { get; init; }

        [JsonPropertyName("night")]
        public decimal? Night { get; init; }

        [JsonPropertyName("eve")]
        public decimal? Eve { get; init; }

        [JsonPropertyName("morn")]
        public decimal? Morn { get; init; }
    }

    public record FeelsLikeChild
    {
        [JsonPropertyName("day")]
        public decimal? Day { get; init; }

        [JsonPropertyName("night")]
        public decimal? Night { get; init; }

        [JsonPropertyName("eve")]
        public decimal? Eve { get; init; }

        [JsonPropertyName("morn")]
        public decimal? Morn { get; init; }
    }

};