using Fincompare.Application.Repositories;
using Fincompare.Domain.Enums;
using Fincompare.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;
using static Fincompare.Application.Request.CustomerUsedCouponRequest.CustomerUsedCouponViewRequest;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerUsedCouponController : ControllerBase
    {
        private readonly ICustomerUsedCouponService _customerUsedCouponService;

        public CustomerUsedCouponController(ICustomerUsedCouponService customerUsedCouponService)
        {
            _customerUsedCouponService = customerUsedCouponService;
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPost]
        [Route("customer-used-coupons")]
        public async Task<IActionResult> CustomerUsedCoupons(CreateCustomerUsedCouponRequest model)
        {
            var response = await _customerUsedCouponService.CustomerUsedCoupons(model);
            return Ok(response);
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPost]
        [Route("fetch-used-coupons")]
        public async Task<IActionResult> GetAllCustomerUsedCoupons
            (
            int? merchantId,
            int? customerId,
            DateTime? startDateTime,
            DateTime? endDateTime,
            string? sendCountry,
            string? receiveCountry,
            string? sendCurrency,
            string? receiveCurrency,
            int? merchantProductID,
            int? serviceCategoryId,
            int? instrumentId,
            bool? isUsed
            )
        {
            var response = await _customerUsedCouponService.GetAllCustomerUsedCoupons
                (
                      merchantId,
                      customerId,
                      startDateTime,
                      endDateTime,
                      sendCountry,
                      receiveCountry,
                      sendCurrency,
                      receiveCurrency,
                      merchantProductID,
                      serviceCategoryId,
                      instrumentId,
                      isUsed
                );
            return Ok(response);
        }
    }
}
