using NanoNet.Services.ShoppingCartAPI.Dtos;

namespace NanoNet.Services.ShoppingCartAPI.Interfaces.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
