using Microsoft.Extensions.Options;
using NanoNet.Services.OrderAPI.Interfaces.IService;
using NanoNet.Services.OrderAPI.Services;
using NanoNet.Services.OrderAPI.Utility;
using NanoNet.Services.ShoppingCartAPI.SettingData;

namespace NanoNet.Services.OrderAPI.ServicesExtension
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var apiData = serviceProvider.GetRequiredService<IOptions<APIsUrl>>().Value;

            services.AddHttpContextAccessor();

            services.AddHttpClient("Product", client =>
            {
                client.BaseAddress = new Uri(apiData.ProductAPI);
            }).AddHttpMessageHandler<AuthenticationHttpClientHandler>();

            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}