using WeatherForecast.Infrastructure.Adapters.Database.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using StackExchange.Redis;
using WeatherForecast.Domain.AggregateModel.WeatherAggregate.Enums;

namespace WeatherForecast.Infrastructure.Adapters.Database.Extensions;

/// <summary>
/// extensions around <see cref="Domain.AggregateModel.WeatherAggregate.WeatherForecast"/> domain model
/// </summary>
public static class DbMappers
{
    /// <summary>
    /// the used serialization options for db interactions
    /// </summary>
    private static JsonSerializerOptions _jsonSerializationOptions = new JsonSerializerOptions
            {
                // useful for converting scale enum into string scale property
                Converters = { new JsonStringEnumConverter() }
            };

    /// <summary>
    /// Converts a domain <see cref="Domain.AggregateModel.WeatherAggregate.WeatherForecast"/> to json format
    /// </summary>
    /// <param name="weatherForecast">a domain weatehr forecast object</param>
    /// <returns>json representation of the weather forecast object</returns>
    public static string ToJson(this Domain.AggregateModel.WeatherAggregate.WeatherForecast weatherForecast)
        => JsonSerializer.Serialize(
            weatherForecast.ToDbObject(), _jsonSerializationOptions);
    

    /// <summary>
    /// Maps a domain weather forecast to a db weather forecast object
    /// </summary>
    /// <param name="weatherForecast">a domain weather forecast object</param>
    /// <returns>a db object, to be stored into database</returns>
    public static DbWeatherForecast ToDbObject(this Domain.AggregateModel.WeatherAggregate.WeatherForecast weatherForecast)
        => new DbWeatherForecast(weatherForecast);

    /// <summary>
    /// Maps a redis value into the original <see cref="DbWeatherForecast"/> object
    /// </summary>
    /// <param name="redisValue">an object got using redis stack exchange library</param>
    /// <returns>db representation of a database object</returns>
    public static DbWeatherForecast ToDbObject(this RedisValue redisValue) 
        => JsonSerializer.Deserialize<DbWeatherForecast>(redisValue.ToString(), _jsonSerializationOptions);

    /// <summary>
    /// Maps a db forecast (redis value) and the city (redis key) to a full valid Weather forecast domain object
    /// </summary>
    /// <param name="city">concerned city (stroed as a redis key)</param>
    /// <param name="dbWeatherForecast">weather foreecast object (stored as json in redis db)</param>
    /// <returns></returns>
    public static Domain.AggregateModel.WeatherAggregate.WeatherForecast ToDomainObject(this DbWeatherForecast dbWeatherForecast, string city)
        => new Domain.AggregateModel.WeatherAggregate.WeatherForecast(
                city, 
                dbWeatherForecast.Temperature, 
                Enum.Parse<MeasurementScale>(dbWeatherForecast.Scale),
                dbWeatherForecast.Summary);
}
