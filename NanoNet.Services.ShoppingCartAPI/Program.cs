using Microsoft.EntityFrameworkCore;
using NanoNet.Services.ShoppingCartAPI.Data;
using NanoNet.Services.ShoppingCartAPI.ServicesExtension;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

builder.Services.AddControllers();

builder.Services.AddSwaggerServices();

builder.Services.AddShoppingCartConfigurations(builder.Configuration);

builder.Services.ConfigureAppsettingData(builder.Configuration);

builder.Services.AddJwtConfigurations();

builder.Services.AddApplicationServices();

#endregion;

var app = builder.Build();

#region Update Database With Using Way And Seeding Data

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var _shoppingContext = services.GetRequiredService<CartDbContext>();

var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
    await _shoppingContext.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "an error has been occured during apply the migration!");
}

#endregion

#region Configure the Kestrel pipeline

app.UseSwaggerMiddleware();

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

#endregion

app.Run();