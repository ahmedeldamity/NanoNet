using Microsoft.EntityFrameworkCore;
using NanoNet.Services.OrderApi.ServicesExtension;
using NanoNet.Services.OrderAPI.Data;
using NanoNet.Services.OrderAPI.ErrorHandling;
using NanoNet.Services.OrderAPI.ServicesExtension;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

builder.Services.AddControllers();

builder.Services.AddSwaggerServices();

builder.Services.ConfigureAppsettingData(builder.Configuration);

builder.Services.AddJWTConfigurations(builder.Configuration);

builder.Services.AddOrderConfigurations(builder.Configuration);

builder.Services.AddApplicationServices();

#endregion

var app = builder.Build();

#region Update Database With Using Way And Seeding Data

using var scope = app.Services.CreateScope()
    ;
var services = scope.ServiceProvider;

var _orderContext = services.GetRequiredService<OrderDbContext>();

var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
    await _orderContext.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "an error has been occured during apply the migration!");
}

#endregion

#region Configure the Kestrel pipeline

app.UseMiddleware<GlobalExceptionHandling>();

app.UseSwaggerMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();