﻿namespace NanoNet.Services.OrderAPI.Dtos;
public class CartDto
{
    public CartHeaderDto CartHeader { get; set; } = null!;
    public IEnumerable<CartItemDto> CartItems { get; set; } = [];
}