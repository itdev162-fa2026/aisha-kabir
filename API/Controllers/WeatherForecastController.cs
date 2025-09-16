using Domain; // Use the WeatherForecast model from Domain project
using Persistence;
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

    private readonly DataContext _context;

    // Constructor: ASP.NET automatically injects a logger when it creates this controller
    public WeatherForecastController(ILogger<WeatherForecastController> logger, DataContext context)
    {
        _logger = logger; // store logger in a private field
        _context = context;
    }

    // // Responds to GET requests at /WeatherForecast
    // [HttpGet(Name = "GetWeatherForecast")]

    [HttpPost]
    public ActionResult<WeatherForecast> Create(){
        Console.WriteLine($"Database path: {_context.DbPath}");
        Console.WriteLine("Insert a new WeatherForecast");

        var forecast = new WeatherForecast(){
            Date = new DateOnly(),
            TemperatureC = 75,
            Summary = "Warm"
        };

        _context.WeatherForecasts.Add(forecast);
        var success = _context.SaveChanges() > 0;

        if (success)
        {
            return forecast;
        }

        throw new Exception("Error creating WeatherForecast");

    }

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