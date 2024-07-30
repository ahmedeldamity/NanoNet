using Microsoft.AspNetCore.Mvc;
using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.ViewModels;
using Newtonsoft.Json;

namespace NanoNet.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

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

            return View(list);
        }

		public async Task<IActionResult> CouponCreate()
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
					return RedirectToAction(nameof(CouponIndex));
				}
			}

			return View(couponModel);
		}
	}
}
