using Microsoft.AspNetCore.Mvc;
using NanoNet.Services.ShoppingCartAPI.Dtos;
using NanoNet.Services.ShoppingCartAPI.ErrorHandling;

namespace NanoNet.Services.ShoppingCartAPI.Interfaces.IService;
public interface ICouponService
{
    Task<CouponDto> GetCouponByCode(string couponCode);
    Task<Result<CartDto>> GetCart(string userId);
    Task<Result<CartDto>> CartUpsert(CartDto cartDto);
    Task<Result<bool>> RemoveCartItem(int cartItemId);
    Task<Result<bool>> ApplyOrRemoveCoupon(CartDto cartDto);
    Task<Result<bool>> EmailCartRequest(CartDto cartDto);
}