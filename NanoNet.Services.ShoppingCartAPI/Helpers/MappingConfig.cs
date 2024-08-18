using AutoMapper;
using NanoNet.Services.ShoppingCartAPI.Dtos;
using NanoNet.Services.ShoppingCartAPI.Models;

namespace NanoNet.Services.ShoppingCartAPI.Helpers
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<CartHeader, CartHeaderDto>()
                .ReverseMap();

            CreateMap<CartItem, CartItemDto>()
                .ReverseMap();
        }
    }
}
