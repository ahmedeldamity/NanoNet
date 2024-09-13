using NanoNet.Services.ProductAPI.SettingData;
using NanoNet.Services.ShoppingCartAPI.SettingData;

namespace NanoNet.Services.OrderAPI.ServicesExtension;
public static class ConfigurationClassesExtension
{
    public static IServiceCollection ConfigureAppsettingData(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<APIsUrl>(configuration.GetSection("ServiceUrls"));

        services.Configure<JWTData>(configuration.GetSection("jwtOptions"));

        return services;
    }
}