using WeatherForecast.Domain.AggregateModel.WeatherAggregate.Enums;

namespace WeatherForecast.Domain.AggregateModel.WeatherAggregate;

/// <summary>
/// Aggregate root for weather forecast model
/// </summary>
public class WeatherForecast
{
    #region ctor

    public WeatherForecast(string city, int temparature, MeasurementScale measurementScale, string summary)
    {
        City = city;
        Temperature = temparature;
        Scale = measurementScale;
        Summary = summary;
    }

    #endregion

    #region Props

    public int Temperature { get; init; }
    public string City { get; init; }
    public MeasurementScale Scale { get; init; }

    public string Summary { get; init; }

    #endregion
}

