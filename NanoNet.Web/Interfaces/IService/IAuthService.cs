using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Interfaces.IService
{
    public interface IAuthService
    {
        Task<ResponseViewModel?> LoginAsync(LoginRequestViewModel model);
        Task<ResponseViewModel?> RegisterAsync(LoginRequestViewModel model);
        Task<ResponseViewModel?> AssignRoleAsync(LoginRequestViewModel model);
    }
}
