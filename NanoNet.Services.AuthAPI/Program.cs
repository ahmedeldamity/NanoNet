using Microsoft.EntityFrameworkCore;
using NanoNet.Services.AuthAPI.Data;
using NanoNet.Services.AuthAPI.ServicesExtension;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

// Register API Controller
builder.Services.AddControllers();

// Register Required Services For Swagger In Extension Method
builder.Services.AddSwaggerServices();

// Add Identity Context and Configurations
builder.Services.AddIdentityConfigurations(builder.Configuration);

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

// --> Bring Object Of IdentityContext For Update His Migration
var _identiyContext = services.GetRequiredService<IdentityContext>();
// --> Bring Object Of ILoggerFactory For Good Show Error In Console    
var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
	// Migrate IdentityContext
	await _identiyContext.Database.MigrateAsync();
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
	// -- Add Swagger Middelwares In Extension Method
	app.UseSwaggerMiddleware();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // has token?

app.UseAuthorization();  // is allowed to enter this end point?

app.MapControllers();

#endregion

app.Run();
