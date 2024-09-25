using Microsoft.EntityFrameworkCore;
using NanoNet.Services.CouponAPI.Data;
using NanoNet.Services.CouponAPI.ErrorHandling;
using NanoNet.Services.CouponAPI.ServicesExtension;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

builder.Services.AddControllers();

builder.Services.AddSwaggerServices();

builder.Services.AddCouponConfigurations(builder.Configuration);

builder.Services.ConfigureAppsettingData(builder.Configuration);

builder.Services.AddJWTConfigurations(builder.Configuration);

builder.Services.AddApplicationServices();

#endregion

var app = builder.Build();

#region Update Database With Using Way And Seeding Data

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var _couponContext = services.GetRequiredService<CouponDbContext>();

var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
    await _couponContext.Database.MigrateAsync();

    await CouponDbContextSeed.SeedProductDataAsync(_couponContext);
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

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

#endregion

app.Run();
