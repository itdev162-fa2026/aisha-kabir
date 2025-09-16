using Domain; // Use the WeatherForecast model from Domain project
using Microsoft.AspNetCore.Mvc; // Provides attributes and base classes for APIs

namespace API.Controllers;

// Marks this class as an API controller
[ApiController]

// Sets the route: [controller] becomes "WeatherForecast"
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    // Static list of weather descriptions
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    // Logger to record information or errors
    private readonly ILogger<WeatherForecastController> _logger;

    // Constructor: ASP.NET automatically injects a logger when it creates this controller
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger; // store logger in a private field
    }

    // Responds to GET requests at /WeatherForecast
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        // Generate 5 random forecasts
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            // Forecast date = today + index
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),

            // Random Celsius temperature between -20 and 55
            TemperatureC = Random.Shared.Next(-20, 55),

            // Random summary from the list
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray(); // Convert results into an array and return
    }
}