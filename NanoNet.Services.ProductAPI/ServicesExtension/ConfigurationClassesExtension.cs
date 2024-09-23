using NanoNet.Services.ProductAPI.SettingData;

namespace NanoNet.Services.ProductAPI.ServicesExtension;
public static class ConfigurationClassesExtension
{
    public static IServiceCollection ConfigureAppsettingData(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtData>(configuration.GetSection("jwtOptions"));

        return services;
    }

}