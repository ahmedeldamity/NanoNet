using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.Services;

namespace NanoNet.Web.ServicesExtension
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddHttpClient<ICouponService, CouponService>();
            services.AddHttpClient<IAuthService, AuthService>();
            
            services.AddScoped(typeof(IBaseService), typeof(BaseService));
            services.AddScoped(typeof(ICouponService), typeof(CouponService));
            services.AddScoped(typeof(IAuthService), typeof(AuthService));

            return services;
        }
    }
}
