using StackExchange.Redis;

namespace WeatherForecast.Api.Domain.Ports;

public interface IRedisConnector
{
    IDatabase WriteDb { get; init; }
    IDatabase ReadDb { get; init; }
    IServer WriteServer { get; init; }
    IServer ReadServer { get; init; }
}