using AutoMapper;
using NanoNet.Services.OrderAPI.Dtos;
using NanoNet.Services.OrderAPI.Models;

namespace NanoNet.Services.OrderAPI.Helpers;
public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<CartHeaderDto, OrderHeaderDto>();

        CreateMap<CartItemDto, OrderItemsDto>()
            .ForMember(x => x.ProductName, y => y.MapFrom(z => z.Product!.Name))
            .ForMember(x => x.Price, y => y.MapFrom(z => z.Product!.Price));

        CreateMap<OrderHeaderDto, OrderHeader>();
    }
}