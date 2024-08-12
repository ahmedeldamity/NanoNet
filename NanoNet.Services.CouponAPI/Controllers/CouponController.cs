using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoNet.Services.CouponAPI.Data;
using NanoNet.Services.CouponAPI.Dtos;
using NanoNet.Services.CouponAPI.Models;

namespace NanoNet.Services.CouponAPI.Controllers
{
    [Authorize]
    public class CouponController: BaseController
    {
        private readonly CouponDbContext _couponDbContext;
        private readonly IMapper _mapper;
        private ResponseDto _responseDto;

        public CouponController(CouponDbContext couponDbContext, IMapper mapper)
        {
            _couponDbContext = couponDbContext;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public ActionResult<ResponseDto> GetAllCoupons()
        {
            try
            {
                IEnumerable<Coupon> coupons = _couponDbContext.Coupons.ToList();
                _responseDto.Result = coupons;
                _responseDto.Result = _mapper.Map<IEnumerable<CouponDto>>(coupons);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpGet("{id:int}")]
        public ActionResult<ResponseDto> GetCouponById(int id)
        {
            try
            {
                var coupon = _couponDbContext.Coupons.First(c => c.CouponId == id);

                _responseDto.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpGet("GetCouponByCode{code}")]
        public ActionResult<ResponseDto> GetCouponByCode(string code)
        {
            try
            {
                var coupon = _couponDbContext.Coupons.First(c => c.CouponCode.ToLower() == code.ToLower());

                _responseDto.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ActionResult<ResponseDto> AddCoupon([FromBody] CouponDto couponDto)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDto);
                _couponDbContext.Add(coupon);
                _couponDbContext.SaveChanges();

                _responseDto.Result = coupon;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ActionResult<ResponseDto> UpdateCoupon([FromBody] CouponDto couponDto)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDto);
                _couponDbContext.Update(coupon);
                _couponDbContext.SaveChanges();

                _responseDto.Result = coupon;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public ActionResult<ResponseDto> DeleteCoupon(int id)
        {
            try
            {
                var coupon = _couponDbContext.Coupons.First(c => c.CouponId == id);
                _couponDbContext.Remove(coupon);
                _couponDbContext.SaveChanges();
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
