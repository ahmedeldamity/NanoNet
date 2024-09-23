using AutoMapper;
using NanoNet.Services.ProductAPI.Dtos;
using NanoNet.Services.ProductAPI.Models;

namespace NanoNet.Services.ProductAPI.Helpers;
public class MappingConfig: Profile
{
    public MappingConfig()
    {
        CreateMap<Product, ProductRequest>()
            .ReverseMap();
    }

}