using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Interfaces.IService
{
    public interface IProductService
    {
        Task<ResponseViewModel?> GetAllProductsAsync();
        Task<ResponseViewModel?> GetProductByIdAsync(int productId);
    }
}
