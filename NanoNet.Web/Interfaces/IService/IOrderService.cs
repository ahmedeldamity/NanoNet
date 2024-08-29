using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Interfaces.IService;
public interface IOrderService
{
    Task<ResponseViewModel?> CreateOrderAsync(CartViewModel cartViewModel);
}