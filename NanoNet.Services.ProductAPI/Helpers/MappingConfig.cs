using AutoMapper;
using NanoNet.Services.ProductAPI.Dtos;
using NanoNet.Services.ProductAPI.Models;

namespace NanoNet.Services.ProductAPI.Helpers;
public class MappingConfig: Profile
{
    public MappingConfig()
    {
        CreateMap<ProductRequest, Product>()
            .ReverseMap();

        CreateMap<Product, ProductResponse>()
            .ReverseMap();
    }

}