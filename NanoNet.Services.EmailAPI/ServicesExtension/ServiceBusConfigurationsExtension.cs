using NanoNet.Services.EmailAPI.Interfaces;

namespace NanoNet.Services.EmailAPI.ServicesExtension;
public static class ServiceBusConfigurationsExtension
{
    private static IAzureServiceBusConsumer _serviceBusConsumer { get; set; } = null!;
    public static IApplicationBuilder AddAzureServiceBusConfigurations(this IApplicationBuilder app)
    {
        _serviceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>()!;

        var lifeTime = app.ApplicationServices.GetService<IHostApplicationLifetime>()!;

        lifeTime.ApplicationStarted.Register(OnStarted);

        lifeTime.ApplicationStopping.Register(OnStopping);

        return app;
    }

    private static void OnStarted()
    {
        _serviceBusConsumer.Start();
    }

    private static void OnStopping() 
    {
        _serviceBusConsumer.Stop();
    }
}