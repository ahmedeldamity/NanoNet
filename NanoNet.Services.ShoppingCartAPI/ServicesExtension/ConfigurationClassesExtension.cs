using NanoNet.Services.ShoppingCartAPI.SettingData;

namespace NanoNet.Services.ShoppingCartAPI.ServicesExtension
{
    public static class ConfigurationClassesExtension
    {
        public static IServiceCollection ConfigureAppsettingData(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTData>(configuration.GetSection("jwtOptions"));

            return services;
        }
    }
}
