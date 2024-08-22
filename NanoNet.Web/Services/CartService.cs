using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.Utility;
using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Services;
public class CartService: ICartService
{
    private readonly IBaseService _baseService;

    public CartService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseViewModel?> GetCartByUserIdAsync(string userId)
    {
        return await _baseService.SendAsync(new RequestViewModel()
        {
            ApiType = SD.ApiType.GET,
            Url = SD.CartAPIBase + $"/api/cart/GetCart/{userId}"
        });
    }

    public async Task<ResponseViewModel?> UpsertCartAsync(CartViewModel cartViewModel)
    {
        return await _baseService.SendAsync(new RequestViewModel()
        {
            ApiType = SD.ApiType.POST,
            Data = cartViewModel,
            Url = SD.CartAPIBase + $"/api/cart/CartUpsert"
        });
    }

    public async Task<ResponseViewModel?> RemoveCartItemAsync(int cartItemId)
    {
        return await _baseService.SendAsync(new RequestViewModel()
        {
            ApiType = SD.ApiType.POST,
            Data = cartItemId,
            Url = SD.CartAPIBase + $"/api/cart/RemoveCartItem"
        });
    }

    public async Task<ResponseViewModel?> ApplyOrRemoveCouponAsync(CartViewModel cartViewModel)
    {
        return await _baseService.SendAsync(new RequestViewModel()
        {
            ApiType = SD.ApiType.POST,
            Data = cartViewModel,
            Url = SD.CartAPIBase + "/api/cart/ApplyOrRemoveCoupon"
        });
    }

    public async Task<ResponseViewModel?> EmailCart(CartViewModel cartViewModel)
    {
        return await _baseService.SendAsync(new RequestViewModel()
        {
            ApiType = SD.ApiType.POST,
            Data = cartViewModel,
            Url = SD.CartAPIBase + "/api/cart/EmailCartRequest"
        });
    }
}
