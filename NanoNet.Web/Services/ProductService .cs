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

        public async Task<ResponseViewModel?> CreateProductAsync(ProductViewModel productDto)
        {
            return await _baseService.SendAsync(new RequestViewModel()
            {
                ApiType = SD.ApiType.POST,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/product",
            });
        }

        public async Task<ResponseViewModel?> UpdateProductAsync(ProductViewModel productDto)
        {
            return await _baseService.SendAsync(new RequestViewModel()
            {
                ApiType = SD.ApiType.PUT,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/product"
            });
        }

        public async Task<ResponseViewModel?> DeleteProductAsync(int id)
        {
            return await _baseService.SendAsync(new RequestViewModel()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + $"/api/product/{id}"
            });
        }
    }
}
