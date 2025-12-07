using DomainWeatherForecast = WeatherForecast.Domain.AggregateModel.WeatherAggregate.WeatherForecast;

namespace WeatherForecast.Application.Services.Interfaces;

public interface IWeatherForecastService
{
    /// <summary>
    /// Fetchs all weather forecast entities from db
    /// </summary>
    /// <param name="weatherForecast"></param>
    /// <returns></returns>
    Task<IEnumerable<DomainWeatherForecast>> GetAll(CancellationToken cancellationToken);
}
