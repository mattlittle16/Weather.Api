
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace ServiceTests
{
    public class Startup 
    {
        private IConfiguration? _configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => true;

            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            _configuration = configurationBuilder.Build();
            
            var envSettings = new EnvironmentSettings();
            _configuration.GetSection("AppSettings").Bind(envSettings);

        
            services.AddHttpClient(Constants.TestClient, options =>
            {
                options.BaseAddress = new Uri(envSettings.Url!);
                options.DefaultRequestHeaders.Add("x-api-key", envSettings.ApiKey!);
            })
            .ConfigurePrimaryHttpMessageHandler(x => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli

            });        
        }
    }    
}