using Microsoft.AspNetCore.Mvc;
using NanoNet.Services.ShoppingCartAPI.Dtos;
using NanoNet.Services.ShoppingCartAPI.ErrorHandling;
using NanoNet.Services.ShoppingCartAPI.Interfaces.IService;

namespace NanoNet.Services.ShoppingCartAPI.Controllers;
public class CartController(ICouponService couponService) : BaseController
{
    [HttpGet("GetCart/{userId}")]
    public async Task<ActionResult<Result>> GetCart(string userId)
    {
        var result = await couponService.GetCart(userId);

        return result;
    }

    [HttpPost("CartUpsert")]
    public async Task<ActionResult<Result>> CartUpsert(CartDto cartDto)
    {
        var result = await couponService.CartUpsert(cartDto);

        return result;
    }

    [HttpPost("RemoveCartItem")]
    public async Task<ActionResult<Result>> RemoveCartItem([FromBody] int cartItemId)
    {
        var result = await couponService.RemoveCartItem(cartItemId);

        return result;
    }

    [HttpPost("ApplyOrRemoveCoupon")]
    public async Task<ActionResult<Result>> ApplyOrRemoveCoupon(CartDto cartDto)
    {
        var result = await couponService.ApplyOrRemoveCoupon(cartDto);

        return result;
    }

    [HttpPost("EmailCartRequest")]
    public async Task<ActionResult<Result>> EmailCartRequest(CartDto cartDto)
    {
        var result = await couponService.EmailCartRequest(cartDto);

        return result;
    }
}