using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NanoNet.Services.ProductAPI.Data;
using NanoNet.Services.ProductAPI.Dtos;
using NanoNet.Services.ProductAPI.Models;

namespace NanoNet.Services.ProductAPI.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ProductDbContext _productDbContext;
        private readonly IMapper _mapper;
        private ResponseDto _responseDto;

        public ProductController(ProductDbContext productDbContext, IMapper mapper)
        {
            _productDbContext = productDbContext;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public ActionResult<ResponseDto> GetAllCoupons()
        {
            try
            {
                IEnumerable<Product> coupons = _productDbContext.Products.ToList();
                _responseDto.Result = coupons;
                _responseDto.Result = _mapper.Map<IEnumerable<ProductDto>>(coupons);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
    }
}
