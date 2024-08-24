using NanoNet.Services.EmailAPI.Interfaces;
using NanoNet.Services.EmailAPI.Messaging;

namespace NanoNet.Services.EmailAPI.ServicesExtension;
public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();

        return services;
    }
}