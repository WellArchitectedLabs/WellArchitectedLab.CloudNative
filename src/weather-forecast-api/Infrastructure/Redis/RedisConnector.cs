using StackExchange.Redis;
using WeatherForecast.Api.Domain.Ports;
using Microsoft.Extensions.Options;
using WeatherForecast.Domain.Ports.Config;

namespace WeatherForecast.Infrastructure.Redis;

public class RedisConnector : IRedisConnector
{
    private readonly Lazy<ConnectionMultiplexer> _readConnection;
    private readonly Lazy<ConnectionMultiplexer> _writeConnection;

    public IDatabase ReadDb => _readConnection.Value.GetDatabase();
    public IDatabase WriteDb => _writeConnection.Value.GetDatabase();

    public IServer ReadServer => _readConnection.Value.GetServer(
        _readConnection.Value.GetEndPoints().First());

    public IServer WriteServer => _writeConnection.Value.GetServer(
        _writeConnection.Value.GetEndPoints().First());

    public RedisConnector(IOptionsMonitor<WeatherForecastConfig> configuration)
    {
        var readConn = configuration.CurrentValue.Redis?.ReadDbConnection;
        var writeConn = configuration.CurrentValue.Redis?.WriteDbConnection;

        if (string.IsNullOrWhiteSpace(readConn) || string.IsNullOrWhiteSpace(writeConn))
            throw new ArgumentNullException($@" Please provide valid Redis configuration:
                            - {nameof(RedisConfig.ReadDbConnection)}
                            - {nameof(RedisConfig.WriteDbConnection)}");

        _readConnection = new Lazy<ConnectionMultiplexer>(() =>
            ConnectionMultiplexer.Connect(readConn));

        _writeConnection = new Lazy<ConnectionMultiplexer>(() =>
            ConnectionMultiplexer.Connect(writeConn));
    }
}
