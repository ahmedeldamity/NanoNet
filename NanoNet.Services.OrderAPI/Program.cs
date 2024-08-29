using NanoNet.Services.OrderApi.ServicesExtension;
using NanoNet.Services.ShoppingCartAPI.ServicesExtension;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

// Register API Controller
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddSwaggerServices();

// Register Order Context
builder.Services.AddOrderConfigurations(builder.Configuration);

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