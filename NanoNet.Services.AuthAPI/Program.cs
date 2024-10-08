using Microsoft.EntityFrameworkCore;
using NanoNet.Services.AuthAPI.Data;
using NanoNet.Services.AuthAPI.ServicesExtension;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

builder.Services.AddControllers();

builder.Services.AddSwaggerServices();

builder.Services.AddIdentityConfigurations(builder.Configuration);

builder.Services.AddApplicationServices(builder.Configuration);

#endregion

var app = builder.Build();

#region Update Database With Using Way And Seeding Data

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var _identityContext = services.GetRequiredService<IdentityContext>();

var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
	await _identityContext.Database.MigrateAsync();
}
catch (Exception ex)
{
	var logger = loggerFactory.CreateLogger<Program>();
	logger.LogError(ex, "an error has been occured during apply the migration!");
}

#endregion

#region Configure the Kestrel pipeline

app.UseSwaggerMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();
