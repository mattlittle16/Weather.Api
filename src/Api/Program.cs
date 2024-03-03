using System.Net;
using Api.ApplicationLogic;
using Api.Configuration;
using Api.Middleware;
using Core.Configuration;
using Core.Constants;
using Core.Interfaces;
using Infrastructure.ExternalServices;
using Infrastructure.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

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

builder.Services.AddHttpClient(Constants.OpenWeatherApi, options =>
    {
        options.BaseAddress = new Uri("http://api.openweathermap.org");
    })
    .ConfigurePrimaryHttpMessageHandler(x => new HttpClientHandler
    {
        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli
    });

builder.Services.AddMemoryCache();

builder.Services.AddRateLimit();

var conString = builder.Configuration.GetConnectionString("WeatherDb");
builder.Services.AddDbContextPool<WeatherDbContext>(
      options => options.UseMySql(conString, ServerVersion.Create(Version.Parse("8.3.0"), ServerType.MySql), b => b.MigrationsAssembly("Infrastructure"))
);

//add services
builder.Services.AddScoped<IOpenWeatherApi, OpenWeatherApi>();
builder.Services.AddScoped<IWeatherService, WeatherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.f
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<DailyRequestLimitMiddleware>();

app.UseCors(Constants.CORSPolicy);

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRateLimiter();

app.Run();
