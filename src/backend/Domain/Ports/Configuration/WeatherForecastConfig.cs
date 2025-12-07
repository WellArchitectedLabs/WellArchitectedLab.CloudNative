using System;

namespace WeatherForecast.Domain.Ports.Config;

public class WeatherForecastConfig
{
    public RedisConfig? Redis { get; set; }
}

public class RedisConfig
{
    public string? RedisReadDbAddress { get; set; }
    public string? RedisWriteDbAddress { get; set; }
}
