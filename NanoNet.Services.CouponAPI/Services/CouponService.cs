using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NanoNet.Services.CouponAPI.Data;
using NanoNet.Services.CouponAPI.Dtos;
using NanoNet.Services.CouponAPI.ErrorHandling;
using NanoNet.Services.CouponAPI.Interfaces;
using NanoNet.Services.CouponAPI.Models;

namespace NanoNet.Services.CouponAPI.Services;
public class CouponService(CouponDbContext _couponDbContext, IMapper mapper, IConfiguration _configuration): ICouponService
{
    public async Task<Result<IReadOnlyList<CouponDto>>> GetAllCoupons()
    {
        var coupons = await _couponDbContext.Coupons.ToListAsync();

        var couponsResponse = mapper.Map<IReadOnlyList<Coupon>, IReadOnlyList<CouponDto>>(coupons);

        return Result.Success(couponsResponse);
    }

    public async Task<Result<CouponDto>> GetCouponById(int id)
    {
        var coupon = await _couponDbContext.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);

        if (coupon is null)
            return Result.Failure<CouponDto>("The coupon you are looking for does not exist. Please check the coupon ID and try again.");

        var couponResponse = mapper.Map<Coupon, CouponDto>(coupon);

        return Result.Success(couponResponse);
    }

    public async Task<Result<CouponDto>> GetCouponByCode(string code)
    {
        var coupon = await _couponDbContext.Coupons.FirstOrDefaultAsync(c =>  c.CouponCode.ToLower() == code.ToLower());

        if (coupon is null)
            return Result.Failure<CouponDto>("The coupon you are looking for does not exist. Please check the coupon ID and try again.");

        var couponResponse = mapper.Map<CouponDto>(coupon);

        return Result.Success(couponResponse);
    }

    public async Task<Result<CouponDto>> AddCoupon(CouponDto couponDto)
    {
        var coupon = mapper.Map<Coupon>(couponDto);

        if (coupon is null)
            return Result.Failure<CouponDto>("The coupon you are trying to add is invalid. Please check the coupon details and try again.");

        await _couponDbContext.AddAsync(coupon);

        await _couponDbContext.SaveChangesAsync();

        Stripe.StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];

        var options = new Stripe.CouponCreateOptions
        {
            Currency = "usd",
            Id = couponDto.CouponCode,
            Name = couponDto.CouponCode,
            AmountOff = (long)(couponDto.DiscountAmount * 100)
        };

        var service = new Stripe.CouponService();

        await service.CreateAsync(options);

        var couponResponse = mapper.Map<CouponDto>(coupon);

        return Result.Success(couponResponse);
    }

    public async Task<Result<CouponDto>> UpdateCoupon(CouponDto couponDto)
    {
        var coupon = mapper.Map<Coupon>(couponDto);

        if (coupon is null)
            return Result.Failure<CouponDto>("The coupon you are trying to add is invalid. Please check the coupon details and try again.");

        _couponDbContext.Update(coupon);

        await _couponDbContext.SaveChangesAsync();

        var couponResponse = mapper.Map<CouponDto>(coupon);

        return Result.Success(couponResponse);
    }

    public async Task<Result> DeleteCoupon(int id)
    {
        var coupon = await _couponDbContext.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);

        if (coupon is null)
            return Result.Failure("The coupon you are looking for does not exist. Please check the coupon ID and try again.");

        _couponDbContext.Remove(coupon);

        await _couponDbContext.SaveChangesAsync();

        Stripe.StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];

        var service = new Stripe.CouponService();

        await service.DeleteAsync(coupon.CouponCode);

        return Result.Success("Coupon deleted successfully");
    }

}