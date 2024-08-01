using NanoNet.Services.AuthAPI.ServicesExtension;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

// Register API Controller
builder.Services.AddControllers();

// Register Required Services For Swagger In Extension Method
builder.Services.AddSwaggerServices();

// Add Identity Context and Configurations
builder.Services.AddIdentityConfigurations(builder.Configuration);

#endregion

var app = builder.Build();

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
