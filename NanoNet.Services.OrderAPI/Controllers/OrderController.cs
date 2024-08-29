using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoNet.Services.OrderAPI.Data;
using NanoNet.Services.OrderAPI.Dtos;
using NanoNet.Services.OrderAPI.Models;

namespace NanoNet.Services.OrderAPI.Controllers
{
    public class OrderController(IMapper _mapper, OrderDbContext _orderDbContext) : BaseController
    {
        [Authorize]
        [HttpPost("CreateOrder")]
        public async Task<ResponseDto> CreateOrder(CartDto cartDto)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                OrderHeaderDto orderHeaderDto = _mapper.Map<CartHeaderDto, OrderHeaderDto>(cartDto.CartHeader);

                orderHeaderDto.OrderItems = _mapper.Map<IEnumerable<CartItemDto>, IEnumerable<OrderItemsDto>>(cartDto.CartItems);

                OrderHeader orderHeader = _mapper.Map<OrderHeaderDto, OrderHeader>(orderHeaderDto);

                await _orderDbContext.OrderHeaders.AddAsync(orderHeader);
                await _orderDbContext.SaveChangesAsync();

                orderHeaderDto.Id = orderHeader.Id;

                response.Result = orderHeaderDto;



                if (orderHeader != null)
                {
                    orderHeader.OrderTime = DateTime.Now;
                    orderHeader.Status = SD.Status_Pending;
                    orderHeader.UserId = _claim.UserId;
                    orderHeader.UserName = _claim.UserName;

                    _db.OrderHeaders.Add(orderHeader);
                    await _db.SaveChangesAsync();

                    response.Result = orderHeader.Id;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "OrderHeader object is null";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return response;
        }
    }
}
