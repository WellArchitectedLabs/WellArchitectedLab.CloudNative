using StackExchange.Redis;

namespace WeatherForecast.Infrastructure.Adapters.Database.Factories;

public static class RedisKeyUtils
{
    public const string WeatherForecastBaseKey = "WeatherForecast";

    public static string CreateWeatherForecastKey(string city)
    {
        var sb = new System.Text.StringBuilder(WeatherForecastBaseKey.Length + city.Length + 1);
        sb.Append(WeatherForecastBaseKey);
        sb.Append(':');
        sb.Append(city);
        return sb.ToString();
    }
    
    public static string ResolveWeatherForecastCity(RedisKey redisKey)
    {
        var key = redisKey.ToString();
        var prefixLength = WeatherForecastBaseKey.Length + 1; // +1 for the colon
        return key.Substring(prefixLength);
    }
}
