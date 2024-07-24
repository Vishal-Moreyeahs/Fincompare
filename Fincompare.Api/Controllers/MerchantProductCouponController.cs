using Fincompare.Application.Repositories;
using Fincompare.Domain.Enums;
using Fincompare.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;
using static Fincompare.Application.Request.MerchantProductCouponRequest.MerchantProductRequestViewModel;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantProductCouponController : ControllerBase
    {
        private readonly IMerchantProductCouponService _merchantProductCouponService;

        public MerchantProductCouponController(IMerchantProductCouponService merchantProductCouponService)
        {
            _merchantProductCouponService = merchantProductCouponService;
        }

        [HasPermission(PermissionEnum.CanAccessMerchant)]
        [HttpPost]
        [Route("add-merchant-product-coupons")]
        public async Task<IActionResult> CreateMerchantProductCoupons(List<CreateMerchantProductCouponRequest> model)
        {
            var response = await _merchantProductCouponService.CreateMerchantProductCoupons(model);
            return Ok(response);
        }


        [HttpGet]
        [Route("fetch-merchant-product-coupons")]
        public async Task<IActionResult> GetAllMerchantProductCoupons
            (
            int merchantId,
            string? merchantCouponBatch,
            int? merchantProductId,
            int? serviceCategoryId,
            int? instrumentId,
            int? productId,
            string? sendCountry,
            string? receiveCountry,
            string? sendCurrency,
            string? receiveCurrency,
            bool? IsUsed,
            bool? status

            )
        {
            var response = await _merchantProductCouponService.GetAllMerchantProductCoupons
                (
                merchantId,
                merchantCouponBatch,
                merchantProductId,
                serviceCategoryId,
                instrumentId,
                productId,
                sendCountry,
                receiveCountry,
                sendCurrency,
                receiveCurrency,
                IsUsed,
                status);
            return Ok(response);
        }

        [HasPermission(PermissionEnum.CanAccessMerchant)]
        [HttpPut]
        [Route("update-merchant-product-coupons")]
        public async Task<IActionResult> UpdateMerchantProductCoupons(UpdateMerchantProductCouponRequest model)
        {
            var response = await _merchantProductCouponService.UpdateMerchantProductCoupons(model);
            return Ok(response);
        }
    }
}
