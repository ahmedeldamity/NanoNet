using NanoNet.Services.OrderAPI.Dtos;
using NanoNet.Services.OrderAPI.Interfaces.IService;
using Newtonsoft.Json;

namespace NanoNet.Services.OrderAPI.Services;

public class ProductService(IHttpClientFactory _httpClientFactory) : IProductService
{
    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        HttpClient client = _httpClientFactory.CreateClient("Product");
        var response = await client.GetAsync("/api/product");
        var content = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<ResponseDto>(content);
        if (data is not null && data.IsSuccess)
        {
            // Map the data to the ProductDto
            var products = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(data.Result));
            return products;
        }
        return new List<ProductDto>();
    }
}