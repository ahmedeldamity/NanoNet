using NanoNet.Services.ShoppingCartAPI.Dtos;
using NanoNet.Services.ShoppingCartAPI.Interfaces.IService;
using Newtonsoft.Json;

namespace NanoNet.Services.ShoppingCartAPI.Services;
public class ProductService(IHttpClientFactory httpClientFactory) : IProductService
{
    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        using var client = httpClientFactory.CreateClient("Product");

        var response = await client.GetAsync("/api/product");

        var content = await response.Content.ReadAsStringAsync();

        var data = JsonConvert.DeserializeObject<ResponseDto>(content);

        if (data is null || !data.IsSuccess) return new List<ProductDto>();

        var products = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(data.Value) ?? string.Empty);

        return products ?? new List<ProductDto>();
    }
}