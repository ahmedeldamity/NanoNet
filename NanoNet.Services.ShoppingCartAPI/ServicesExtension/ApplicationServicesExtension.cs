using Microsoft.Extensions.Options;
using NanoNet.MessageBus;
using NanoNet.Services.ShoppingCartAPI.Helpers;
using NanoNet.Services.ShoppingCartAPI.Interfaces.IService;
using NanoNet.Services.ShoppingCartAPI.Services;
using NanoNet.Services.ShoppingCartAPI.SettingData;
using NanoNet.Services.ShoppingCartAPI.Utility;

namespace NanoNet.Services.ShoppingCartAPI.ServicesExtension;
public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var apiData = serviceProvider.GetRequiredService<IOptions<ApisUrl>>().Value;

        services.AddHttpContextAccessor();

        services.AddHttpClient("Product", client =>
        {
            client.BaseAddress = new Uri(apiData.ProductApi);
        }).AddHttpMessageHandler<AuthenticationHttpClientHandler>();

        services.AddHttpClient("Coupon", client =>
        {
            client.BaseAddress = new Uri(apiData.CouponApi);
        }).AddHttpMessageHandler<AuthenticationHttpClientHandler>();

        services.AddAutoMapper(typeof(MappingConfig));

        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<ICouponService, CouponService>();

        services.AddScoped<AuthenticationHttpClientHandler>();

        services.AddScoped<IMessageBusService, MessageBusService>();

        return services;
    }
}