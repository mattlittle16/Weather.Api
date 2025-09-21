using Api.ApplicationLogic;
using Api.Configuration;
using Api.Validators;
using Core.Configuration;
using Core.Interfaces;
using Core.RequestModels;
using FluentValidation;
using Infrastructure.ExternalServices;
using Infrastructure.Interfaces;
using Refit;

namespace Api.Startup;

public static class Services
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, EnvironmentSettings envSettings)
    {   
        //Refit
        services.AddRefitClient<IOpenWeatherApiRefit>().ConfigureHttpClient(options => options.BaseAddress = new Uri(envSettings.OpenWeatherApiBaseUrl!));

        //caching and rate limiting
        services.AddMemoryCache();
        services.AddRateLimit();

        //add services
        services.AddScoped<IOpenWeatherApi, OpenWeatherApi>();
        services.AddScoped<IWeatherService, WeatherService>();
        services.AddScoped<IValidator<WeatherRequestModel>, WeatherRequestValidator>();
        services.AddScoped<IValidator<GeocodeRequestModel>, GeocodeRequestValidator>();
        services.AddScoped<IValidator<ReverseGeocodeRequestModel>, ReverseGeocodeRequestValidator>();

        
        return services;
    }
}