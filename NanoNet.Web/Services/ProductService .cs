using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.Utility;
using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Services
{
    public class ProductService(IBaseService _baseService) : IProductService
    {
        public async Task<ResponseViewModel?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestViewModel()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product"
            });
        }

        public async Task<ResponseViewModel?> GetProductByIdAsync(int productId)
        {
            return await _baseService.SendAsync(new RequestViewModel()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + $"/api/product/{productId}"
            });
        }
    }
}
