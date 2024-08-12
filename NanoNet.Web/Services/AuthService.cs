using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.Utility;
using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Services
{
    public class AuthService(IBaseService _baseService) : IAuthService
    {
        public async Task<ResponseViewModel?> AssignRoleAsync(RegistrationRequestViewModel model)
        {
            return await _baseService.SendAsync(new RequestViewModel()
            {
                ApiType = SD.ApiType.POST,
                Data = model,
                Url = SD.AuthAPIBase + "/api/auth/AssignRole"
            });
        }

        public async Task<ResponseViewModel?> LoginAsync(LoginRequestViewModel model)
        {
            return await _baseService.SendAsync(new RequestViewModel()
            {
                ApiType = SD.ApiType.POST,
                Data = model,
                Url = SD.AuthAPIBase + "/api/auth/login"
            }, withBearer: false /* To make SendAsync function not bring token because we not put it yet */);
        }

        public async Task<ResponseViewModel?> RegisterAsync(RegistrationRequestViewModel model)
        {
            return await _baseService.SendAsync(new RequestViewModel()
            {
                ApiType = SD.ApiType.POST,
                Data = model,
                Url = SD.AuthAPIBase + "/api/auth/register"
            }, withBearer: false /* To make SendAsync function not bring token because we not put it yet */);
        }
    }
}
