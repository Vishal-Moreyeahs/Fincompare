using Fincompare.Application.Repositories;
using Fincompare.Domain.Enums;
using Fincompare.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;
using static Fincompare.Application.Request.CouponRequest.CouponRequestViewModel;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPost]
        [Route("add-coupon")]
        public async Task<IActionResult> CreateCoupon(CreateCouponRequest model)
        {
            var response = await _couponService.CreateCoupon(model);
            return Ok(response);
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPut]
        [Route("update-coupon")]
        public async Task<IActionResult> UpdateCoupon(UpdateCouponRequest model)
        {
            var response = await _couponService.UpdateCoupon(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("fetch-coupon")]
        public async Task<IActionResult> GetAllCoupon(int? couponId, bool? status)
        {
            var response = await _couponService.GetAllCoupon(couponId, status);
            return Ok(response);
        }
    }
}
