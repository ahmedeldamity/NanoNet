using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NanoNet.Services.ShoppingCartAPI.Data;
using NanoNet.Services.ShoppingCartAPI.Dtos;
using NanoNet.Services.ShoppingCartAPI.Models;

namespace NanoNet.Services.ShoppingCartAPI.Controllers
{
    public class CartController(CartDbContext _shoppingDbContext, IMapper _mapper) : BaseController
    {
        private readonly ResponseDto _responseDto = new ResponseDto();

        [HttpPost]
        public async Task<IActionResult> CartUpsert(CartDto cartDto)
        {
            // CartHeader.UserId
            try
            {
                var cartHeader = await _shoppingDbContext.CartHeaders.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UserId == cartDto.CartHeader.UserId);

                if (cartHeader == null)
                {
                    // create header and Items

                    // create cart header
                    var cartHeaderForAdd = _mapper.Map<CartHeaderDto, CartHeader>(cartDto.CartHeader);
                    _shoppingDbContext.CartHeaders.Add(cartHeaderForAdd);
                    await _shoppingDbContext.SaveChangesAsync();

                    // create cart Item
                    cartDto.CartItems.First().CartHeaderId = cartHeaderForAdd.Id;
                    var cartItemForAdd = _mapper.Map<CartItemDto, CartItem>(cartDto.CartItems.First());
                    _shoppingDbContext.CartItems.Add(cartItemForAdd);
                    await _shoppingDbContext.SaveChangesAsync();
                }
                else
                {
                    // we sure now that header is not null so we can check if Items exist
                    var cartItems = await _shoppingDbContext.CartItems.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.ProductId == cartDto.CartItems.First().ProductId && x.CartHeaderId == cartHeader.Id);

                    if (cartItems == null)
                    {
                        // The first time we add this product to the cart
                        // create cart Item
                        cartDto.CartItems.First().CartHeaderId = cartHeader.Id;
                        var cartItemForAdd = _mapper.Map<CartItemDto, CartItem>(cartDto.CartItems.First());
                        _shoppingDbContext.CartItems.Add(cartItemForAdd);
                        await _shoppingDbContext.SaveChangesAsync();
                    }
                    else
                    {
                        // update cart Item
                        cartDto.CartItems.First().Count += cartItems.Count;
                        cartDto.CartItems.First().CartHeaderId = cartItems.CartHeaderId; /// -delete
                        cartDto.CartItems.First().Id = cartItems.Id; /// -delete
                        _shoppingDbContext.CartItems.Update(_mapper.Map<CartItemDto, CartItem>(cartDto.CartItems.First()));
                        await _shoppingDbContext.SaveChangesAsync();
                    }
                }
                _responseDto.Result = cartDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }
            return Ok(_responseDto);
        }
    }
}
