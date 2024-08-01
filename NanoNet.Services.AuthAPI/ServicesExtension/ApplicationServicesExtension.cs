using NanoNet.Services.AuthAPI.Interfaces.IService;
using NanoNet.Services.AuthAPI.Models;
using NanoNet.Services.AuthAPI.Services;

namespace NanoNet.Services.AuthAPI.ServicesExtension
{
	public static class ApplicationServicesExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped(typeof(IAuthService), typeof(AuthService));

			services.AddScoped<IJWTTokenGenerator, JWTTokenGenerator>();

			services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

			return services;
		}
	}
}
