using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        public ActionResult<ResponseDto> GetAllProducts()
        {
            try
            {
                IEnumerable<Product> products = _productDbContext.Products.ToList();
                _responseDto.Result = products;
                _responseDto.Result = _mapper.Map<IEnumerable<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpGet("{id:int}")]
        public ActionResult<ResponseDto> GetProductById(int id)
        {
            try
            {
                var product = _productDbContext.Products.First(c => c.Id == id);

                _responseDto.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPost]
        public ActionResult<ResponseDto> AddProduct([FromBody] ProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                _productDbContext.Add(product);
                _productDbContext.SaveChanges();

                _responseDto.Result = product;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPut]
        public ActionResult<ResponseDto> UpdateProduct([FromBody] ProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                _productDbContext.Update(product);
                _productDbContext.SaveChanges();

                _responseDto.Result = product;
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
