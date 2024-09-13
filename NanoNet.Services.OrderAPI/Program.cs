using Microsoft.EntityFrameworkCore;
using NanoNet.Services.OrderApi.ServicesExtension;
using NanoNet.Services.OrderAPI.Data;
using NanoNet.Services.OrderAPI.ServicesExtension;
using NanoNet.Services.ShoppingCartAPI.ServicesExtension;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

// Register API Controller
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddSwaggerServices();

// Configure Appsetting Data
builder.Services.ConfigureAppsettingData(builder.Configuration);

// Register JWT Configuration
builder.Services.AddJWTConfigurations(builder.Configuration);

// Register Order Context
builder.Services.AddOrderConfigurations(builder.Configuration);

// This Method Has All Application Services
builder.Services.AddApplicationServices();

#endregion

var app = builder.Build();

#region Update Database With Using Way And Seeding Data

// We Said To Update Database You Should Do Two Things (1. Create Instance From DbContext 2. Migrate It)

// To Ask Clr To Create Instance Explicitly From Any Class
//    1 ->  Create Scope (Life Time Per Request)
using var scope = app.Services.CreateScope();
//    2 ->  Bring Service Provider Of This Scope
var services = scope.ServiceProvider;

// --> Bring Object Of OrderDbContext For Update His Migration And Data Seeding
var _orderContext = services.GetRequiredService<OrderDbContext>();

// --> Bring Object Of ILoggerFactory For Good Show Error In Console    
var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
    // Migrate OrderDbContext
    await _orderContext.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "an error has been occured during apply the migration!");
}

#endregion

#region Configure the Kestrel pipeline

if (app.Environment.IsDevelopment())
{
    // Add Swagger middleware
    app.UseSwaggerMiddleware();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();