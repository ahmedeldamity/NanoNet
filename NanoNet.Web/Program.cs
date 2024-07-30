using NanoNet.Web.ServicesExtension;
using NanoNet.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

// Register MVC Controller
builder.Services.AddControllersWithViews();

// This Method Has All Application Services
builder.Services.AddApplicationServices();

// This Method Give The Unity Class Properties Their Values 
builder.Configuration.AddPropertiesValueForUnityClass();

#endregion

var app = builder.Build();

#region Configure the Kestrel pipeline

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// -- To Redirect Any Http Request To Https
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

#endregion

app.Run();
