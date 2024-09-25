﻿namespace NanoNet.Services.OrderAPI.Dtos;
public class OrderItemsDto
{
    public int Id { get; set; }
    public int OrderHeaderId { get; set; }
    public int ProductId { get; set; }
    public ProductDto? Product { get; set; }
    public int Count { get; set; }
    public string ProductName { get; set; } = null!;
    public double Price { get; set; }
}