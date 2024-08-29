using NanoNet.Services.OrderApi.ServicesExtension;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

// Register API Controller
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddSwaggerServices();

#endregion

var app = builder.Build();

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