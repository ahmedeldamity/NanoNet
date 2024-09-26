using Microsoft.Extensions.Options;
using NanoNet.Services.OrderAPI.Helpers;
using NanoNet.Services.OrderAPI.Interfaces;
using NanoNet.Services.OrderAPI.Interfaces.IService;
using NanoNet.Services.OrderAPI.Services;
using NanoNet.Services.OrderAPI.SettingData;
using NanoNet.Services.OrderAPI.Utility;

namespace NanoNet.Services.OrderAPI.ServicesExtension;
public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();

        var apiData = serviceProvider.GetRequiredService<IOptions<APIsUrl>>().Value;

        services.AddScoped<IOrderService, OrderService>();

        services.AddScoped<IProductService, ProductService>();

        services.AddHttpContextAccessor();

        services.AddHttpClient("Product", client =>
        {
            client.BaseAddress = new Uri(apiData.ProductAPI);
        }).AddHttpMessageHandler<AuthenticationHttpClientHandler>();

        services.AddAutoMapper(typeof(MappingConfig));

        return services;
    }
}