using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Interfaces.IService
{
    public interface IBaseService
    {
        Task<ResponseViewModel?> SendAsync(RequestViewModel requestDto, bool withBearer = true);
    }
}
