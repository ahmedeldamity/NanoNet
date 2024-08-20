using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Interfaces.IService;
public interface ICartService
{
    Task<ResponseViewModel?> GetCartByUserIdAsync(string userId);
    Task<ResponseViewModel?> UpsertCartAsync(CartViewModel cartViewModel);
}