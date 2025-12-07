using DomainWeatherForecast = WeatherForecast.Domain.AggregateModel.WeatherAggregate.WeatherForecast;

namespace WeatherForecast.Api.Dtos.WeatherForecast.Get;

/// <summary>
/// Dto related to Get weather forecast controller api method
/// </summary>
/// <param name="Date"></param>
/// <param name="Temperature"></param>
/// <param name="Scale"></param>
/// <param name="Summary"></param>
public record WeatherForecastGetDto(DateOnly Date, int Temperature, string Scale, string Summary);