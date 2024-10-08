﻿using NanoNet.Services.ShoppingCartAPI.Dtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace NanoNet.Services.ShoppingCartAPI.Models;
public class CartItem
{
    public int Id { get; set; }

    public int CartHeaderId { get; set; }

    public CartHeader CartHeader { get; set; } = null!;
    
    public int ProductId { get; set; }

    [NotMapped] public ProductDto Product { get; set; } = null!;

    public int Count { get; set; }
}