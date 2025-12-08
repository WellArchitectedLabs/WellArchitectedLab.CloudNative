using WeatherForecast.Api.Domain.Ports;
using WeatherForecast.Application;
using WeatherForecast.Application.Services.Interfaces;
using WeatherForecast.Domain.Ports.Adapters.Database;
using WeatherForecast.Infrastructure.Adapters.Database;
using WeatherForecast.Infrastructure.Redis;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection RegisterLayers(this IServiceCollection services)
    {
        services.RegisterInfrastructureLayer();
        return services.RegisterApplicationLayer();
    }

    public static IServiceCollection RegisterInfrastructureLayer(this IServiceCollection services)
    {
        services.AddSingleton<IRedisConnector, RedisConnector>();
        return services.AddScoped<IWeatherForecastDbAdapter, WeatherForecastRedisDbAdapter>();
    }
    
    public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
    {
        return services.AddScoped<IWeatherForecastService, WeatherForecastService>();
    }
}