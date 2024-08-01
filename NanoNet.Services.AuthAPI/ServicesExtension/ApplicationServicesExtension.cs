using NanoNet.Services.AuthAPI.Interfaces.IService;
using NanoNet.Services.AuthAPI.Services;

namespace NanoNet.Services.AuthAPI.ServicesExtension
{
	public static class ApplicationServicesExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped(typeof(IAuthService), typeof(AuthService));

			return services;
		}
	}
}
