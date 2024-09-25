using NanoNet.Services.OrderAPI.Dtos;
using NanoNet.Services.OrderAPI.ErrorHandling;

namespace NanoNet.Services.OrderAPI.Interfaces;
public interface IOrderService
{
    Task<Result<OrderHeaderDto>> CreateOrder(CartDto cartDto);
    Task<Result<StripeRequestDto>> CreateStripeSession(StripeRequestDto stripeRequestDto);
    Task<Result<OrderHeaderDto>> ValidateStripeSession(int orderHeaderId);
}