using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Api.Dtos.WeatherForecast.Get;
using WeatherForecast.Api.Extensions;
using WeatherForecast.Application.Services.Interfaces;
using WeatherForecast.Domain.Ports.Adapters.Database;

namespace WeatherForecast.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _weatherForecastService;

    /// <summary>
    /// Initializer
    /// </summary>
    /// <param name="weatherForecastService"></param>
    public WeatherForecastController(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet(Name = "GetAll")]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var allForecasts = await _weatherForecastService.GetAll(cancellationToken);
        if(allForecasts.Any())
            return NoContent();
        return Ok(allForecasts.Select(f => f.MapToGetDto()));
    }
}
