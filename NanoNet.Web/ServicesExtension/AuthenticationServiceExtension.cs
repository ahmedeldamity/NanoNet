using Microsoft.AspNetCore.Authentication.Cookies;

namespace NanoNet.Web.ServicesExtension
{
    public static class AuthenticationServiceExtension
    {
        public static IServiceCollection AddAuthenticationConfigurations(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                    options.LoginPath = "/Auth/Login";
                    options.AccessDeniedPath = "/Auth/AccessDenied";
                });

            return services;
        }
    }
}
