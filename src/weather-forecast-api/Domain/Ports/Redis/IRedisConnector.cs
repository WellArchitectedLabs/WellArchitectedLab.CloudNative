using StackExchange.Redis;

namespace WeatherForecast.Api.Domain.Ports;

public interface IRedisConnector
{
    IDatabase WriteDb { get; }
    IDatabase ReadDb { get; }
    IServer WriteServer { get; }
    IServer ReadServer { get; }
}