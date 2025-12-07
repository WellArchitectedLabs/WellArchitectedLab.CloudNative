using StackExchange.Redis;

namespace WeatherForecast.Infrastructure.Adapters.Database.Models;

/// <summary>
/// A weather forecast object as stored in redis database
/// </summary>
public record struct DbWeatherForecast
{
    public DbWeatherForecast(Domain.AggregateModel.WeatherAggregate.WeatherForecast weatherForecastDomainObject)
    {
        Temperature = weatherForecastDomainObject.Temperature;
        Scale = weatherForecastDomainObject.Scale.ToString();
        Summary = weatherForecastDomainObject.Summary;
    }

    public int Temperature { get; init; }
    public string Scale { get; init; }
    public string Summary { get; init; }
}

/// <summary>
/// Used for holding short-lived data when deserializing redis database results
/// </summary>
/// <param name="Key">City value</param>
/// <param name="Value">db stored <see cref="DbWeatherForecast"/> object</param>
public record struct DbWeatherForecastKeyValue(string Key, Task<RedisValue> Value);
