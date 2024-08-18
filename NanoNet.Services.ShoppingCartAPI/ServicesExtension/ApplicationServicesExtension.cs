using Microsoft.Extensions.Options;
using NanoNet.Services.ShoppingCartAPI.Helpers;
using NanoNet.Services.ShoppingCartAPI.Interfaces.IService;
using NanoNet.Services.ShoppingCartAPI.Services;
using NanoNet.Services.ShoppingCartAPI.SettingData;

namespace NanoNet.Services.ShoppingCartAPI.ServicesExtension
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var apiData = serviceProvider.GetRequiredService<IOptions<APIsUrl>>().Value;


            services.AddHttpClient("Product", client =>
            {
                client.BaseAddress = new Uri(apiData.ProductAPI);
            });

            services.AddAutoMapper(typeof(MappingConfig));

            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
