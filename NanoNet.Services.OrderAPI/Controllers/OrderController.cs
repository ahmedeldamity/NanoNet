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
        [HttpPost("create-order")]
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