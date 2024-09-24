using NanoNet.Services.ShoppingCartAPI.SettingData;

namespace NanoNet.Services.ShoppingCartAPI.ServicesExtension;
public static class ConfigurationClassesExtension
{
    public static IServiceCollection ConfigureAppsettingData(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtData>(configuration.GetSection("jwtOptions"));

        // Take setting data form appsetting to APIsUrl class
        services.Configure<ApisUrl>(configuration.GetSection("ServiceUrls"));

        // Take setting data form appsetting to TopicAndQueueData class
        services.Configure<TopicAndQueueNames>(configuration.GetSection("TopicAndQueueNames"));

        return services;
    }
}