using StackExchange.Redis;
using WeatherForecast.Api.Domain.Ports;
using Microsoft.Extensions.Options;
using WeatherForecast.Domain.Ports.Config;

namespace WeatherForecast.Infrastructure.Redis;

public class RedisConnector : IRedisConnector
{
    #region Properties

    public IDatabase WriteDb { get; init; }
    public IDatabase ReadDb { get; init; }
    public IServer ReadServer { get; init; }
    public IServer WriteServer { get; init; }

    #endregion

    public RedisConnector(IOptionsMonitor<WeatherForecastConfig> configuration)
    {
        var readDbConnectionString = configuration.CurrentValue.Redis?.ReadDbConnection;
        var writeConnectionString = configuration.CurrentValue.Redis?.WriteDbConnection;

        if (string.IsNullOrWhiteSpace(readDbConnectionString) || string.IsNullOrWhiteSpace(writeConnectionString))
            throw new ArgumentNullException($@"Please provide valid values for both configurations: 
                                                {nameof(RedisConfig.ReadDbConnection)} 
                                                and {nameof(RedisConfig.WriteDbConnection)}");

        (ReadDb, ReadServer) = GetRedisCredentials(writeConnectionString);
        (WriteDb, WriteServer) = GetRedisCredentials(readDbConnectionString);

    }

    /// <summary>
    /// Creates a redis connection from the given connection string
    /// </summary>
    /// <param name="redisWriteConnectionString">redis server connection string</param>
    /// <returns></returns>
    private (IDatabase, IServer) GetRedisCredentials(string redisWriteConnectionString)
    {
        var writeDbConnection = ConnectionMultiplexer.Connect(redisWriteConnectionString);
        return (writeDbConnection.GetDatabase(), writeDbConnection.GetServer(writeDbConnection.GetEndPoints().First()));
    }
}
