using System;

namespace WeatherForecast.Domain.Ports.Config;

public class WeatherForecastConfig
{
    public RedisConfig? Redis { get; set; }
}

public class RedisConfig
{
    public string? RedisReadDbConnection { get; set; }
    public string? RedisWriteDbConnection { get; set; }
}
