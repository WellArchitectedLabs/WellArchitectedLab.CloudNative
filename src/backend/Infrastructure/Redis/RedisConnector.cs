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

    public RedisConnector(IOptionsSnapshot<WeatherForecastConfig> configuration)
    {
        var redisReadDbConnectionString = configuration.Value.Redis?.RedisReadDbAddress;
        var redisWriteConnectionString = configuration.Value.Redis?.RedisWriteDbAddress;

        if (string.IsNullOrWhiteSpace(redisReadDbConnectionString) || string.IsNullOrWhiteSpace(redisWriteConnectionString))
            throw new ArgumentNullException($@"Please provide valid values for both configurations: 
                                                {nameof(RedisConfig.RedisReadDbAddress)} 
                                                and {nameof(RedisConfig.RedisWriteDbAddress)}");

        (ReadDb, ReadServer) = GetRedisCredentials(redisWriteConnectionString);
        (WriteDb, WriteServer) = GetRedisCredentials(redisReadDbConnectionString);

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
