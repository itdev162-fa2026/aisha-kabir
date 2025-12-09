using Persistence;

// Create a WebApplication builder object.
// This sets up configuration, logging, and the services container.
var builder = WebApplication.CreateBuilder(args);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
    policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Register services (Dependency Injection container).
// Add support for OpenAPI/Swagger (for API docs & testing).
builder.Services.AddOpenApi();

// Add support for controller-based APIs.
// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        // Disable automatic 400 response so we can return 422 for validation errors
        options.SuppressModelStateInvalidFilter = true;
    });
builder.Services.AddDbContext<DataContext>();

// Build the WebApplication object from the configured builder.
var app = builder.Build();

// Use CORS
app.UseCors("AllowReactApp");

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())

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
