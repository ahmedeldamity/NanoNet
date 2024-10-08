﻿using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Interfaces.IService;
public interface ICartService
{
    Task<ResponseViewModel?> GetCartByUserIdAsync(string userId);
    Task<ResponseViewModel?> UpsertCartAsync(CartViewModel cartViewModel);
    Task<ResponseViewModel?> RemoveCartItemAsync(int cartItemId);
    Task<ResponseViewModel?> ApplyOrRemoveCouponAsync(CartViewModel cartViewModel);
    Task<ResponseViewModel?> EmailCart(CartViewModel cartViewModel);
}