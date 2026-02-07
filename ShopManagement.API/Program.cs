using Microsoft.EntityFrameworkCore;
using ShopManagement.API.Middleware;
using ShopManagement.Core.Interfaces;
using ShopManagement.Infrastructure.Data;
using ShopManagement.Infrastructure.Repositories;
using ShopManagement.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container - Demonstrating Dependency Injection configuration

// Add Entity Framework
builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseInMemoryDatabase("ShopManagementDb")); // Using InMemory for demo purposes

// Register repositories - Demonstrating Dependency Injection
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register services - Demonstrating Service Layer pattern with DI
builder.Services.AddScoped<IProductService, ProductService>();

// Add controllers with JSON options to handle circular references
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Add API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() 
    { 
        Title = "Shop Management API", 
        Version = "v1",
        Description = "A comprehensive Shop Management API demonstrating .NET Core 8 concepts including OOP, Middleware, Repository Pattern, Service Layer, and Dependency Injection"
    });
    
    // Include XML comments for better API documentation
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Add CORS for development
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add logging
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline - Demonstrating Middleware pipeline

// Development-specific middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop Management API v1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at root
    });
    app.UseCors("DevelopmentPolicy");
}

// Custom middleware - demonstrating middleware pattern
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Built-in middleware
app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
    context.Database.EnsureCreated();
}

// Display startup information
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Shop Management API started successfully");
logger.LogInformation("Swagger UI available at: {SwaggerUrl}", app.Environment.IsDevelopment() ? "https://localhost:5001" : "Not available in production");

app.Run();