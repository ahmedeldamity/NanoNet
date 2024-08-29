using NanoNet.Services.OrderAPI.Dtos;

namespace NanoNet.Services.OrderAPI.Interfaces.IService;
public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProducts();
}