using Microsoft.AspNetCore.Mvc;
using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.ViewModels;
using Newtonsoft.Json;

namespace NanoNet.Web.Controllers;
public class CouponController(ICouponService _couponService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> CouponIndex()
    {
        List<CouponViewModel>? list = [];

        var response = await _couponService.GetAllCouponsAsync();

        if (response is not null && response.IsSuccess)
        {
            var jsonData = Convert.ToString(response.Value);

			if (string.IsNullOrEmpty(jsonData))
            {
                TempData["error"] = "No coupons found";
                return View(list);
            }

            list = JsonConvert.DeserializeObject<List<CouponViewModel>>(jsonData);
        }
		else
		{
			TempData["error"] = response?.Error;
		}

        return View(list);
    }

	public IActionResult CouponCreate()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> CouponCreate(CouponViewModel couponModel)
	{
        if (ModelState.IsValid is false) 
            return View(couponModel);

        var response = await _couponService.CreateCouponAsync(couponModel);

        if (response is not null && response.IsSuccess)
        {
            TempData["success"] = "Coupon created successfully";

            return RedirectToAction(nameof(CouponIndex));
        }

        TempData["error"] = response?.Error;

        return View(couponModel);
	}

	public async Task<IActionResult> CouponEdit(int couponId)
	{
		var response = await _couponService.GetCouponByIdAsync(couponId);

		if (response?.IsSuccess is true)
		{
			var jsonData = Convert.ToString(response.Value);
            
			if (string.IsNullOrEmpty(jsonData))
            {
                TempData["error"] = "Coupon not found";
                return NotFound();
            }

			var model = JsonConvert.DeserializeObject<CouponViewModel>(jsonData);

			return View(model);
		}

		TempData["error"] = response?.Error;

		return NotFound();
	}

	[HttpPost]
	public async Task<IActionResult> CouponEdit(CouponViewModel couponDto)
	{
        if (ModelState.IsValid is false) 
            return View(couponDto);

        var response = await _couponService.UpdateCouponAsync(couponDto);

        if (response?.IsSuccess is true)
        {
            TempData["success"] = "Coupon updated successfully";

            return RedirectToAction(nameof(CouponIndex));
        }

        TempData["error"] = response?.Error;

        return View(couponDto);
	}

	public async Task<IActionResult> CouponDelete(int couponId)
	{
		var response = await _couponService.GetCouponByIdAsync(couponId);

		if (response is not null && response.IsSuccess)
		{
			var jsonData = Convert.ToString(response.Value);

            if (string.IsNullOrEmpty(jsonData))
            {
                TempData["error"] = "Coupon not found";
                return NotFound();
            }

			var model = JsonConvert.DeserializeObject<CouponViewModel>(jsonData);

            return View(model);
		}
        
        TempData["error"] = response?.Error;

        return NotFound();
	}

	[HttpPost]
	public async Task<IActionResult> CouponDelete(CouponViewModel couponModel)
	{
		var response = await _couponService.DeleteCouponAsync(couponModel.CouponId);

		if (response is not null && response.IsSuccess)
		{
            TempData["success"] = "Coupon deleted successfully";

            return RedirectToAction(nameof(CouponIndex));
		}

        TempData["error"] = response?.Error;

        return View(couponModel);
	}

}