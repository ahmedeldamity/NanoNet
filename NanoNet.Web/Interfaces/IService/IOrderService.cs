using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Interfaces.IService;
public interface IOrderService
{
    Task<ResponseViewModel?> CreateOrderAsync(CartViewModel cartViewModel);
    Task<ResponseViewModel?> CreateStripeSessionAsync(StripeRequestViewModel stripeRequestViewModel);
}