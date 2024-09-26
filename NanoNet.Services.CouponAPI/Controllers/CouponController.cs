using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoNet.Services.CouponAPI.Dtos;
using NanoNet.Services.CouponAPI.ErrorHandling;
using NanoNet.Services.CouponAPI.Interfaces;

namespace NanoNet.Services.CouponAPI.Controllers;

[Authorize]
public class CouponController(ICouponService couponService) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<Result>> GetAllCoupons()
    {
        var result = await couponService.GetAllCoupons();

        return result;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Result>> GetCouponById(int id)
    {
        var result = await couponService.GetCouponById(id);

        return result;
    }

    [HttpGet("GetCouponByCode/{code}")]
    public async Task<ActionResult<Result>> GetCouponByCode(string code)
    {
        var result = await couponService.GetCouponByCode(code);
        
        return result;
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result>> AddCoupon([FromBody] CouponDto couponDto)
    {
        var result = await couponService.AddCoupon(couponDto);

        return result;
    }

    [HttpPut]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result>> UpdateCoupon([FromBody] CouponDto couponDto)
    {
        var result = await couponService.UpdateCoupon(couponDto);

        return result;
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result>> DeleteCoupon(int id)
    {
        var result = await couponService.DeleteCoupon(id);

        return result;
    }
}
