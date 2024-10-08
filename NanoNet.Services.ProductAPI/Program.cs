using Microsoft.EntityFrameworkCore;
using NanoNet.Services.ProductAPI.Data;
using NanoNet.Services.ProductAPI.ErrorHandling;
using NanoNet.Services.ProductAPI.ServicesExtension;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

builder.Services.AddControllers();

builder.Services.AddSwaggerServices();

builder.Services.AddProductConfigurations(builder.Configuration);

builder.Services.ConfigureAppsettingData(builder.Configuration);

builder.Services.AddJwtConfigurations(builder.Configuration);

builder.Services.AddApplicationServices();

#endregion

var app = builder.Build();

#region Update Database With Using Way And Seeding Data

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var productContext = services.GetRequiredService<ProductDbContext>();

var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
    await productContext.Database.MigrateAsync();
    await ProductDbContextSeed.SeedProductDataAsync(productContext, builder.Configuration);
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "an error has been occured during apply the migration!");
}

#endregion

#region Configure the Kestrel pipeline

app.UseMiddleware<GlobalExceptionHandling>();

app.UseStaticFiles();

app.UseSwaggerMiddleware();

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

#endregion

app.Run();
