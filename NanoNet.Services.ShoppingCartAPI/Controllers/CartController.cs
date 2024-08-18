﻿using AutoMapper;
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

        [HttpGet("GetCart/{userId}")]
        public IActionResult GetCart(string userId)
        {
            try
            {
                var cartHeader = _mapper.Map<CartHeaderDto>(_shoppingDbContext.CartHeaders.First(x => x.UserId == userId));

                var cartItems = _mapper.Map<IEnumerable<CartItemDto>>(_shoppingDbContext.CartItems.Where(x => x.CartHeaderId == cartHeader.Id));

                var cartDto = new CartDto
                {
                    CartHeader = cartHeader,
                    CartItems = cartItems
                };

                foreach (var item in cartDto.CartItems)
                {
                    cartDto.CartHeader.TotalPrice += item.Count * item.Product.Price;
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

        [HttpPost("CartUpsert")]
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

        [HttpPost("RemoveCartItem")]
        public async Task<IActionResult> RemoveCartItem([FromBody] int cartItemId)
        {
            try
            {
                var cartItem = await _shoppingDbContext.CartItems.FirstOrDefaultAsync(x => x.Id == cartItemId);

                int cartItemsCount = 0;

                // get total cart items
                if(cartItem != null)
                {
                    cartItemsCount = await _shoppingDbContext.CartItems.Where(x => x.CartHeaderId == cartItem.CartHeaderId).CountAsync();
                    _shoppingDbContext.CartItems.Remove(cartItem);
                    if (cartItemsCount == 1)
                    {
                        var cartHeaderToRemove = await _shoppingDbContext.CartHeaders.FirstOrDefaultAsync(x => x.Id == cartItem.CartHeaderId);
                        _shoppingDbContext.CartHeaders.Remove(cartHeaderToRemove);
                        await _shoppingDbContext.SaveChangesAsync();
                    }
                    await _shoppingDbContext.SaveChangesAsync();
                }
                _responseDto.Result = true;
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
