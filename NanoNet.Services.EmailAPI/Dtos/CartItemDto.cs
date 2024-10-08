﻿namespace NanoNet.Services.EmailAPI.Dtos;
public class CartItemDto
{
    public int Id { get; set; }
    public int CartHeaderId { get; set; }
    public CartHeaderDto? CartHeader { get; set; }
    public int ProductId { get; set; }
    public ProductDto? Product { get; set; }
    public int Count { get; set; }
}