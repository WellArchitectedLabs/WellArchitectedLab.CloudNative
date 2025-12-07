namespace WeatherForecast.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseSwaggerSwashbuckle(this WebApplication webApplication)
    {
        if (webApplication.Environment.IsDevelopment())
        {
            // only activate for development as best practise
            webApplication.UseSwagger();
            webApplication.UseSwaggerUI();
        }
        return webApplication;
    }
}
