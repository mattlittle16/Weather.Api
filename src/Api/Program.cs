using Api.ApplicationLogic;
using Api.Configuration;
using Api.Middleware;
using Core.Configuration;
using Core.Constants;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.ExternalServices;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Refit;
using FluentValidation;
using Api.Validators;
using Core.RequestModels;
using Api.Startup;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog with code-based configuration
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .ReadFrom.Configuration(builder.Configuration) // Read log levels from appsettings
    .WriteTo.Console(new CompactJsonFormatter())
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .Enrich.WithProperty("ApplicationName", "Weather.Api")
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
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
builder.Services.AddSingleton(envSettings);

builder.Services.AddCustomServices(envSettings);



var app = builder.Build();
app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath}{QueryString} responded {StatusCode} in {Elapsed:0.0000} ms";
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("QueryString", httpContext.Request.QueryString.Value);
        if (httpContext.Request.Query.Count > 0)
        {
            diagnosticContext.Set("QueryParameters", httpContext.Request.Query.ToDictionary(x => x.Key, x => x.Value.ToString()));
        }
    };
});

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();

// dev stuff
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName.ToLower() == "docker")
{
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
