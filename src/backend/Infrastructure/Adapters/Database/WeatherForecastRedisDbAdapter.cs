using StackExchange.Redis;
using WeatherForecast.Api.Domain.Ports;
using WeatherForecast.Domain.Ports.Adapters.Database;
using WeatherForecast.Infrastructure.Adapters.Database.Extensions;
using WeatherForecast.Infrastructure.Adapters.Database.Factories;
using WeatherForecast.Infrastructure.Adapters.Database.Models;

namespace WeatherForecast.Infrastructure.Adapters.Database;

public class WeatherForecastRedisDbAdapter : IWeatherForecastDbAdapter
{
    private const int BatchSize = 1000;
    private readonly IRedisConnector _redisConnector;

    public WeatherForecastRedisDbAdapter(IRedisConnector redisConnector)
    {
        _redisConnector = redisConnector;
    }

    #region implementations

    /// </<inheritdoc/>
    public async Task AddOrUpdate(Domain.AggregateModel.WeatherAggregate.WeatherForecast weatherForecast, CancellationToken cancellationToken)
        => await _redisConnector.WriteDb.StringSetAsync(
                RedisKeyUtils.CreateWeatherForecastKey(weatherForecast.City),
                weatherForecast.ToJson()
                );

    /// <summary>
    /// </<inheritdoc/>
    /// Gets all redis data (keys and values)
    /// Implementation is optimized using stack exchange's <see cref="IBatch"/> 
    /// Similar to a select * from a relational database. Please use carefully.
    /// </summary>
    /// <returns>All stored redis key values</returns>
    public async Task<IEnumerable<Domain.AggregateModel.WeatherAggregate.WeatherForecast>> GetAll(CancellationToken cancellationToken)
    {
        var redisKeys = (RedisKey[])_redisConnector.ReadServer.Keys(pattern: "*");

        // use stack exchange batch method for extreme optimization
        var batch = _redisConnector.ReadDb.CreateBatch();

        IEnumerable<DbWeatherForecastKeyValue> byKeyTasks = new List<DbWeatherForecastKeyValue>();

        byKeyTasks = redisKeys.Select(key
            => new DbWeatherForecastKeyValue(key.ToString(), _redisConnector.ReadDb.StringGetAsync(key.ToString())));

        batch.Execute();

        var redisValues = await Task.WhenAll(byKeyTasks.Select(t => t.Value));

        return byKeyTasks.Select(kv =>
            kv.Value.Result.
            // maps the redis value to a db object
            ToDbObject().
            // and then maps the db object to a domain object
            // and passing the city (which is the redis key) as parameter
            ToDomainObject(RedisKeyUtils.ResolveWeatherForecastCity(kv.Key)));
    }

    #endregion
}
