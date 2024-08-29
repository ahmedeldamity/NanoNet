using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.Utility;
using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Services
{
    public class OrderService(IBaseService _baseService) : IOrderService
    {
        public async Task<ResponseViewModel?> CreateOrderAsync(CartViewModel cartViewModel)
        {
            return await _baseService.SendAsync(new RequestViewModel()
            {
                ApiType = SD.ApiType.POST,
                Data = cartViewModel,
                Url = SD.OrderAPIBase + "/api/order/create-order",
            });
        }
    }
}