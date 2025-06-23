using Api.ApplicationLogic;
using Api.Configuration;
using Api.Middleware;
using Core.Configuration;
using Core.Constants;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.ExternalServices;
using Infrastructure.Interfaces;
using Infrastructure.MySql;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Infrastructure.Logger;
using Api.Startup;
using Refit;
using FluentValidation;
using Api.Validators;
using Core.RequestModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<EnvironmentSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddCors(options =>
{
    options.AddPolicy(Constants.CORSPolicy,
        builder =>
        {
            builder.SetIsOriginAllowed(_ => true);
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
            builder.AllowCredentials();
        });
});


var appSettings = builder.Configuration.GetSection("AppSettings");
var envSettings = new EnvironmentSettings();
appSettings.Bind(envSettings);
builder.Services.AddRefitClient<IOpenWeatherApiRefit>().ConfigureHttpClient(options => options.BaseAddress = new Uri(envSettings.OpenWeatherApiBaseUrl));


//caching and rate limiting
builder.Services.AddMemoryCache();
builder.Services.AddRateLimit();

var conString = builder.Configuration.GetConnectionString("WeatherDb");
builder.Services.AddDbContextPool<WeatherDbContext>(
      options => options.UseMySql(conString, ServerVersion.Create(Version.Parse("8.3.0"), ServerType.MySql), b => b.MigrationsAssembly("Infrastructure"))
);

//add services
builder.Services.AddScoped<IOpenWeatherApi, OpenWeatherApi>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IGenericRepository<Base>, GenericRepository<Base>>();
builder.Services.AddScoped<IValidator<WeatherRequestModel>, WeatherRequestValidator>();
builder.Services.AddScoped<IValidator<GeocodeRequestModel>, GeocodeRequestValidator>();

//Logging
builder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider>();

var app = builder.Build();

// dev stuff
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName.ToLower() == "docker")
{
    app.UseItToSeedSqlServer(); 
    app.UseSwagger();
    app.UseSwaggerUI();
}

//middleware
app.UseMiddleware<DailyRequestLimitMiddleware>();

//cors
app.UseCors(Constants.CORSPolicy);

//app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.UseRateLimiter();

app.Run();
