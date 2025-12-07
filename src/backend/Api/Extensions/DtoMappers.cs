using DomainWeatherForecast = WeatherForecast.Domain.AggregateModel.WeatherAggregate.WeatherForecast;
using WeatherForecast.Api.Dtos.WeatherForecast.Get;

namespace WeatherForecast.Api.Extensions;

/// <summary>
/// Extensions methods for all mappers / converters between domain and dto models
/// </summary>
public static class DtoMappers
{
    /// <summary>
    /// Maps a domain <see cref="DomainWeatherForecast"/> to a target get weather forecast API dto <see cref="WeatherForecastGetDto"/>
    /// </summary>
    /// <param name="domainWeatherForecast"></param>
    /// <returns></returns>
    public static WeatherForecastGetDto MapToGetDto(this DomainWeatherForecast domainWeatherForecast)
        => new WeatherForecastGetDto(
            DateOnly.FromDateTime(DateTime.UtcNow),
            domainWeatherForecast.Temperature,
            domainWeatherForecast.Scale.ToString(),
            domainWeatherForecast.Summary);

}
