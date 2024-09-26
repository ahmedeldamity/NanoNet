using Microsoft.EntityFrameworkCore;
using NanoNet.Services.EmailAPI.Data;
using NanoNet.Services.EmailAPI.ErrorHandling;
using NanoNet.Services.EmailAPI.ServicesExtension;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

builder.Services.AddControllers();

builder.Services.AddSwaggerServices();

builder.Services.AddEmailConfigurations(builder.Configuration);

builder.Services.AddApplicationServices();

#endregion

var app = builder.Build();

#region Update Database With Using Way And Seeding Data

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var _emailContext = services.GetRequiredService<EmailDbContext>();

var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
    await _emailContext.Database.MigrateAsync();
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

app.AddAzureServiceBusConfigurations();

#endregion

app.Run();
