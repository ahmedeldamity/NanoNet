using Microsoft.AspNetCore.Mvc;
using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.ViewModels;
using Newtonsoft.Json;

namespace NanoNet.Web.Controllers
{
    public class CouponController(ICouponService _couponService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponViewModel>? list = new();

            ResponseViewModel? response = await _couponService.GetAllCouponsAsync();

            if (response is not null && response.IsSuccess)
            {
                var jsonData = Convert.ToString(response.Result);
                list = JsonConvert.DeserializeObject<List<CouponViewModel>>(jsonData);
            }
			else
			{
				TempData["error"] = response?.Message;
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
            if (ModelState.IsValid)
            {
			    ResponseViewModel? response = await _couponService.CreateCouponAsync(couponModel);

				if (response is not null && response.IsSuccess)
				{
                    TempData["success"] = "Coupon created successfully";
                    return RedirectToAction(nameof(CouponIndex));
				}
                else
                {
                    TempData["error"] = response?.Message;
                }
            }

			return View(couponModel);
		}

		public async Task<IActionResult> CouponDelete(int couponId)
		{
			List<CouponViewModel>? list = new();

			ResponseViewModel? response = await _couponService.GetCouponByIdAsync(couponId);

			if (response is not null && response.IsSuccess)
			{
				var jsonData = Convert.ToString(response.Result);
				var model = JsonConvert.DeserializeObject<CouponViewModel>(jsonData);
                return View(model);
			}
            else
            {
                TempData["error"] = response?.Message;
            }

            return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> CouponDelete(CouponViewModel couponModel)
		{
			ResponseViewModel? response = await _couponService.DeleteCouponAsync(couponModel.CouponId);

			if (response is not null && response.IsSuccess)
			{
                TempData["success"] = "Coupon deleted successfully";
                return RedirectToAction(nameof(CouponIndex));
			}
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(couponModel);
		}
	}
}
