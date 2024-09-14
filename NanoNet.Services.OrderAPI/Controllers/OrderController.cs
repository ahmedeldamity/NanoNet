using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoNet.Services.OrderAPI.Data;
using NanoNet.Services.OrderAPI.Dtos;
using NanoNet.Services.OrderAPI.Models;
using NanoNet.Services.OrderAPI.Utility;
using Stripe.Checkout;
using Stripe;

namespace NanoNet.Services.OrderAPI.Controllers
{
    public class OrderController(IMapper _mapper, OrderDbContext _orderDbContext, IConfiguration _configuration) : BaseController
    {
        [Authorize]
        [HttpPost("create-order")]
        public async Task<ResponseDto> CreateOrder(CartDto cartDto)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                OrderHeaderDto orderHeaderDto = _mapper.Map<CartHeaderDto, OrderHeaderDto>(cartDto.CartHeader);

                orderHeaderDto.OrderItems = _mapper.Map<IEnumerable<CartItemDto>, IEnumerable<OrderItemsDto>>(cartDto.CartItems);

                OrderHeader orderHeader = _mapper.Map<OrderHeaderDto, OrderHeader>(orderHeaderDto);

                orderHeader.OrderTime = DateTime.Now;
                orderHeader.Status = SD.Status_Pending;

                await _orderDbContext.OrderHeaders.AddAsync(orderHeader);
                await _orderDbContext.SaveChangesAsync();

                orderHeaderDto.Id = orderHeader.Id;

                response.Result = orderHeaderDto;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return response;
        }

        [Authorize]
        [HttpPost("CreateStripeSession")]
        public async Task<ResponseDto> CreateStripeSession(StripeRequestDto stripeRequestDto)
        {
            ResponseDto response = new();
            try
            {
                StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];

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
                            UnitAmount = (long) (item.Price*100),
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

                Session session = service.Create(options);

                stripeRequestDto.StripeSessionUrl = session.Url;

                OrderHeader orderHeader = _orderDbContext.OrderHeaders.First(u => u.Id == stripeRequestDto.OrderHeader.Id);

                orderHeader.StripeSessionId = session.Id;

                await _orderDbContext.SaveChangesAsync();

                response.Result = stripeRequestDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [Authorize]
        [HttpPost("ValidateStripeSession")]
        public async Task<ResponseDto> ValidateStripeSession(int orderHeaderId)
        {
            ResponseDto response = new();
            try
            {
                OrderHeader orderHeader = _orderDbContext.OrderHeaders.First(u => u.Id == orderHeaderId);

                var service = new SessionService();

                Session session = service.Get(orderHeader.StripeSessionId);

                var paymentIntentService = new PaymentIntentService();

                var paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

                if (paymentIntent.Status == "succeeded")
                {
                    orderHeader.PaymentIntentId = paymentIntent.Id;

                    orderHeader.Status = SD.Status_Completed;

                    _orderDbContext.SaveChanges();

                    response.Result = _mapper.Map<OrderHeader, OrderHeaderDto>(orderHeader);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

    }
}