namespace WeatherForecast.Domain.Ports.Adapters.Database;

/// <summary>
/// Communication layer between Application and external database providers
/// </summary>
public interface IWeatherForecastDbAdapter
{
   /// <summary>
    /// Adds / Updates the domain object into database 
    /// </summary>
    /// <param name="weatherForecast"><see cref="Domain.AggregateModel.WeatherAggregate.WeatherForecast"/></param>
    /// <param name="cancellationToken">propagates client cancellations to db layer</param>
    /// <returns></returns>
    Task AddOrUpdate(AggregateModel.WeatherAggregate.WeatherForecast weatherForecast, CancellationToken cancellationToken);

    /// <summary>
    /// Gets all databse entities.
    /// Please use carefully.
    /// </summary>
    /// <param name="cancellationToken">propagates client cancellations to db layer</param>
    /// <returns>All stored domain entities from database</returns>
    Task<IEnumerable<AggregateModel.WeatherAggregate.WeatherForecast>> GetAll(CancellationToken cancellationToken);
}
