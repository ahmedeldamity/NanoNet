using NanoNet.Web.ServicesExtension;
using NanoNet.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

builder.Services.AddControllersWithViews();

builder.Services.AddApplicationServices();

builder.Services.ConfigureAppsettingData(builder.Configuration);

builder.Services.AddPropertiesValueForUnityClass(builder.Configuration);

builder.Services.AddAuthenticationConfigurations();

#endregion

var app = builder.Build();

#region Configure the Kestrel pipeline

if (app.Environment.IsDevelopment() is false)
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

#endregion

app.Run();