using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Interfaces.IService
{
    public interface IAuthService
    {
        Task<ResponseViewModel?> LoginAsync(LoginRequestViewModel model);
        Task<ResponseViewModel?> RegisterAsync(RegistrationRequestViewModel model);
        Task<ResponseViewModel?> AssignRoleAsync(RegistrationRequestViewModel model);
    }
}
