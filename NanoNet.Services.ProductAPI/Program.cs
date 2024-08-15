using NanoNet.Services.ProductAPI.ServicesExtension;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

// Register API Controller
builder.Services.AddControllers();

// Register Required Services For Swagger In Extension Method
builder.Services.AddSwaggerServices();

// Register Coupon Context
builder.Services.AddProductConfigurations(builder.Configuration);

#endregion

var app = builder.Build();

#region Configure the Kestrel pipeline

if (app.Environment.IsDevelopment())
{
    // -- Add Swagger Middelwares In Extension Method
    app.UseSwaggerMiddleware();
}

app.UseAuthentication();

app.UseAuthorization();

// -- To Redirect Any Http Request To Https
app.UseHttpsRedirection();

app.MapControllers();

#endregion

app.Run();
