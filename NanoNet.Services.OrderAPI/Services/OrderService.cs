using AutoMapper;
using NanoNet.Services.OrderAPI.Data;
using NanoNet.Services.OrderAPI.Dtos;
using NanoNet.Services.OrderAPI.ErrorHandling;
using NanoNet.Services.OrderAPI.Interfaces;
using NanoNet.Services.OrderAPI.Models;
using NanoNet.Services.OrderAPI.Utility;
using Stripe.Checkout;
using Stripe;

namespace NanoNet.Services.OrderAPI.Services;
public class OrderService(IMapper mapper, OrderDbContext orderDbContext, IConfiguration configuration): IOrderService
{
    public async Task<Result<OrderHeaderDto>> CreateOrder(CartDto cartDto)
    {
        var orderHeaderDto = mapper.Map<CartHeaderDto, OrderHeaderDto>(cartDto.CartHeader);

        if(orderHeaderDto is null)
            return Result.Failure<OrderHeaderDto>("The order you are trying to create is invalid. Please check the order details and try again.");

        orderHeaderDto.OrderItems = mapper.Map<IEnumerable<CartItemDto>, IEnumerable<OrderItemsDto>>(cartDto.CartItems);

        if(orderHeaderDto.OrderItems is null)
            return Result.Failure<OrderHeaderDto>("The order items you are trying to add are invalid. Please check the order items and try again.");

        var orderHeader = mapper.Map<OrderHeaderDto, OrderHeader>(orderHeaderDto);

        orderHeader.OrderTime = DateTime.Now;

        orderHeader.Status = SD.Status_Pending;

        await orderDbContext.OrderHeaders.AddAsync(orderHeader);

        await orderDbContext.SaveChangesAsync();

        orderHeaderDto.Id = orderHeader.Id;

        return Result.Success(orderHeaderDto);
    }

    public async Task<Result<StripeRequestDto>> CreateStripeSession(StripeRequestDto stripeRequestDto)
    {
        StripeConfiguration.ApiKey = configuration["StripeSettings:Secretkey"];

        var options = new SessionCreateOptions
        {
            SuccessUrl = stripeRequestDto.StripeApprovedUrl,
            CancelUrl = stripeRequestDto.StripeCancelledUrl,
            LineItems = [],
            Mode = "payment",
        };

        foreach (var item in stripeRequestDto.OrderHeader.OrderItems)
        {
            var sessionLineItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(item.Price * 100),
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Product!.Name
                    }
                },
                Quantity = item.Count
            };
            options.LineItems.Add(sessionLineItem);
        }

        var service = new SessionService();

        var session = await service.CreateAsync(options);

        stripeRequestDto.StripeSessionUrl = session.Url;

        var orderHeader = orderDbContext.OrderHeaders.
            First(u => u.Id == stripeRequestDto.OrderHeader.Id);

        orderHeader.StripeSessionId = session.Id;

        await orderDbContext.SaveChangesAsync();

        return Result.Success(stripeRequestDto);
    }

    public async Task<Result<OrderHeaderDto>> ValidateStripeSession(int orderHeaderId)
    {
        var orderHeader = orderDbContext.OrderHeaders.First(u => u.Id == orderHeaderId);

        var service = new SessionService();

        var session = await service.GetAsync(orderHeader.StripeSessionId);

        var paymentIntentService = new PaymentIntentService();

        var paymentIntent = await paymentIntentService.GetAsync(session.PaymentIntentId);

        if (paymentIntent.Status != "succeeded")
            return Result.Failure<OrderHeaderDto>("The payment was not successful. Please try again.");

        orderHeader.PaymentIntentId = paymentIntent.Id;

        orderHeader.Status = SD.Status_Completed;

        await orderDbContext.SaveChangesAsync();

        var orderHeaderDto = mapper.Map<OrderHeader, OrderHeaderDto>(orderHeader);

        return Result.Success(orderHeaderDto);
    }

}