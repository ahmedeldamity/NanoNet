using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Interfaces.IService;
public interface IProductService
{
    Task<ResponseViewModel?> GetAllProductsAsync();
    Task<ResponseViewModel?> GetProductByIdAsync(int productId);
    Task<ResponseViewModel?> CreateProductAsync(ProductViewModel productDto);
    Task<ResponseViewModel?> UpdateProductAsync(ProductViewModel productDto);
    Task<ResponseViewModel?> DeleteProductAsync(int id);
}