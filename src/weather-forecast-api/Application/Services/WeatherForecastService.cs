using WeatherForecast.Application.Services.Interfaces;
using WeatherForecast.Domain.Ports.Adapters.Database;

namespace WeatherForecast.Application;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IWeatherForecastDbAdapter _weatherForecastDbAdapter;
    public WeatherForecastService(IWeatherForecastDbAdapter weatherForecastDbAdapter)
    {
        _weatherForecastDbAdapter = weatherForecastDbAdapter;
    }

    public Task<IEnumerable<Domain.AggregateModel.WeatherAggregate.WeatherForecast>> GetAll(CancellationToken cancellationToken)
        => _weatherForecastDbAdapter.GetAll(cancellationToken);
}
