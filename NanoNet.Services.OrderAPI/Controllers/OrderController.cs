using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoNet.Services.OrderAPI.Dtos;
using NanoNet.Services.OrderAPI.ErrorHandling;
using NanoNet.Services.OrderAPI.Interfaces;

namespace NanoNet.Services.OrderAPI.Controllers;
public class OrderController(IOrderService orderService) : BaseController
{
    [Authorize]
    [HttpPost("create-order")]
    public async Task<ActionResult<Result>> CreateOrder(CartDto cartDto)
    {
        var result = await orderService.CreateOrder(cartDto);

        return result;
    }

    [Authorize]
    [HttpPost("CreateStripeSession")]
    public async Task<ActionResult<Result>> CreateStripeSession(StripeRequestDto stripeRequestDto)
    {
        var result = await orderService.CreateStripeSession(stripeRequestDto);

        return result;
    }
    
    [Authorize]
    [HttpPost("ValidateStripeSession")]
    public async Task<ActionResult<Result>> ValidateStripeSession(int orderHeaderId)
    {
        var result = await orderService.ValidateStripeSession(orderHeaderId);

        return result;
    }
}