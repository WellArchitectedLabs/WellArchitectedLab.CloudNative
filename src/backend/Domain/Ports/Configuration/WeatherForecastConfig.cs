using System;

namespace WeatherForecast.Domain.Ports.Config;

public class WeatherForecastConfig
{
    public RedisConfig? Redis { get; set; }
}

public class RedisConfig
{
    public string? ReadDbConnection { get; set; }
    public string? WriteDbConnection { get; set; }
}
