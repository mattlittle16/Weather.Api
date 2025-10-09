using Core.ApplicationLogic;
using Api.Configuration;
using Api.Validators;
using Core.Configuration;
using Core.Interfaces;
using Core.RequestModels;
using FluentValidation;
using Infrastructure.ExternalServices;
using Infrastructure.Interfaces;
using Refit;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Core.Decorators;

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

        //base services
        services.AddScoped<IOpenWeatherApi, OpenWeatherApi>();
        services.AddScoped<WeatherRepository>();
        services.AddScoped<GeocodeRepository>();

        //deorated services
        services.AddScoped<IWeatherRepository>(provider =>
        {
            var baseRepo = provider.GetRequiredService<WeatherRepository>();
            var cache = provider.GetRequiredService<IMemoryCache>();
            var logger = provider.GetRequiredService<ILogger<CachedWeatherRepository>>();
            return new CachedWeatherRepository(baseRepo, cache, logger);
        });

        services.AddScoped<IGeocodeRepository>(provider =>
        {
            var baseRepo = provider.GetRequiredService<GeocodeRepository>();
            var cache = provider.GetRequiredService<IMemoryCache>();
            var logger = provider.GetRequiredService<ILogger<CachedGeocodeRepository>>();
            return new CachedGeocodeRepository(baseRepo, cache, logger);
        });

        //application services
        services.AddScoped<IWeatherService, WeatherService>();
        services.AddScoped<IGeocodeService, GeocodeService>();
        services.AddScoped<IValidator<WeatherRequestModel>, WeatherRequestValidator>();
        services.AddScoped<IValidator<GeocodeRequestModel>, GeocodeRequestValidator>();
        services.AddScoped<IValidator<ReverseGeocodeRequestModel>, ReverseGeocodeRequestValidator>();


        return services;
    }
}