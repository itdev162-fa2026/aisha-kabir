using Persistence;

// Create a WebApplication builder object.
// This sets up configuration, logging, and the services container.
var builder = WebApplication.CreateBuilder(args);

// Register services (Dependency Injection container).
// Add support for OpenAPI/Swagger (for API docs & testing).
builder.Services.AddOpenApi();

// Add support for controller-based APIs.
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>();

// Build the WebApplication object from the configured builder.
var app = builder.Build();

// Configure the HTTP request pipeline (how requests are handled).
if (app.Environment.IsDevelopment())
{
    // If running in development mode, enable OpenAPI/Swagger endpoints.
    app.MapOpenApi();
}

// Map controller endpoints (e.g., /WeatherForecast).
app.MapControllers();

// Start the web server and listen for incoming requests.
app.Run();







// // app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

// app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
