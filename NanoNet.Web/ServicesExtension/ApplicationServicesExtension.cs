using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.Services;

namespace NanoNet.Web.ServicesExtension;
public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddHttpClient();
        
        services.AddScoped(typeof(IBaseService), typeof(BaseService));
        services.AddScoped(typeof(ICouponService), typeof(CouponService));
        services.AddScoped(typeof(IAuthService), typeof(AuthService));
        services.AddScoped(typeof(ITokenProvider), typeof(TokenProvider));
        services.AddScoped(typeof(IProductService), typeof(ProductService));
        services.AddScoped(typeof(ICartService), typeof(CartService));

        return services;
    }
}
