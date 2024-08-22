using NanoNet.Services.ShoppingCartAPI.SettingData;

namespace NanoNet.Services.ShoppingCartAPI.ServicesExtension;
public static class ConfigurationClassesExtension
{
    public static IServiceCollection ConfigureAppsettingData(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JWTData>(configuration.GetSection("jwtOptions"));

        // Take setting data form appsetting to APIsUrl class
        services.Configure<APIsUrl>(configuration.GetSection("ServiceUrls"));

        // Take setting data form appsetting to TopicAndQueueData class
        services.Configure<AzureServiceBusData>(configuration.GetSection("AzureServiceBus"));

        return services;
    }
}