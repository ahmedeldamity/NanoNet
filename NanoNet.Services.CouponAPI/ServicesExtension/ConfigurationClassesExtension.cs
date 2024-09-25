using NanoNet.Services.CouponAPI.SettingData;

namespace NanoNet.Services.CouponAPI.ServicesExtension;
public static class ConfigurationClassesExtension
{
    public static IServiceCollection ConfigureAppsettingData(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JWTData>(configuration.GetSection("jwtOptions"));

        return services;
    }
}