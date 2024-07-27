using Microsoft.EntityFrameworkCore;
using NanoNet.Services.CouponAPI.Data;
using NanoNet.Services.CouponAPI.ServicesExtension;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

// Register API Controller
builder.Services.AddControllers();

// Register Required Services For Swagger In Extension Method
builder.Services.AddSwaggerServices();

// Register Coupon Context
builder.Services.AddDbContext<CouponDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

#endregion

var app = builder.Build();

#region Update Database With Using Way And Seeding Data

// We Said To Update Database You Should Do Two Things (1. Create Instance From DbContext 2. Migrate It)

// To Ask Clr To Create Instance Explicitly From Any Class
//    1 ->  Create Scope (Life Time Per Request)
using var scope = app.Services.CreateScope();
//    2 ->  Bring Service Provider Of This Scope
var services = scope.ServiceProvider;

// --> Bring Object Of CouponContext For Update His Migration And Data Seeding
var _couponContext = services.GetRequiredService<CouponDbContext>();

// --> Bring Object Of ILoggerFactory For Good Show Error In Console    
var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
    // Migrate CouponContext
    await _couponContext.Database.MigrateAsync();
    // Seeding Data For CouponContext
    await CouponDbContextSeed.SeedProductDataAsync(_couponContext);
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

// -- To Redirect Any Http Request To Https
app.UseHttpsRedirection();

app.MapControllers();

#endregion

app.Run();
