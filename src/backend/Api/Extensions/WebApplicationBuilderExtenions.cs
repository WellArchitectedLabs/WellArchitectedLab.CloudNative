using WeatherForecast.Domain.Ports.Config;

namespace WeatherForecast.Api.Extensions;

public static class WebApplicationwebApplicationBuilderExtenions
{
    public static void SetupConfiguration(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Configuration.AddEnvironmentVariables();
        webApplicationBuilder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        webApplicationBuilder.Services.Configure<WeatherForecastConfig>(
            webApplicationBuilder.Configuration.GetSection("WeatherForecast"));
    }
}
