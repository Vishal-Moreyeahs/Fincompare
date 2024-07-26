using Fincompare.Api.Middleware;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.MerchantProductRequests;
using Fincompare.Domain.Enums;
using Fincompare.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantProductController : ControllerBase
    {
        private readonly IMerchantProductService _merchantProductService;

        public MerchantProductController(IMerchantProductService merchantProductService)
        {
            _merchantProductService = merchantProductService;
        }

        [HasPermission(PermissionEnum.CanAccessMerchant)]
        [HttpPost]
        [ValidateModelState]
        [Route("add-merchant-product")]
        public async Task<IActionResult> AddMerchantProduct(AddMerchantProductRequest model)
        {
            var response = await _merchantProductService.AddMerchantProduct(model);
            return Ok(response);
        }

        [HasPermission(PermissionEnum.CanAccessMerchant)]
        [HttpPut]
        [ValidateModelState]
        [Route("update-merchant-product")]
        public async Task<IActionResult> UpdateMerchantProduct(UpdateMerchantProductRequest model)
        {
            var response = await _merchantProductService.UpdateMerchantProduct(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("fetch-merchant-product-by-merchantId/{merchantId}")]
        public async Task<IActionResult> GetMerchantProductByMerchantId(int merchantId)
        {
            var response = await _merchantProductService.GetMerchantProductByMerchantId(merchantId);
            return Ok(response);
        }

        [HttpGet("{sendCountry}/{receiveCountry}/{sendCurrency}/{receiveCurrency}")]
        public async Task<IActionResult> GetMerchantProducts(
                                                            string sendCountry,
                                                            string receiveCountry,
                                                            string sendCurrency,
                                                            string receiveCurrency,
                                                            [FromQuery] int? merchantID,
                                                            [FromQuery] int? merchantProductID,
                                                            [FromQuery] int? productID,
                                                            [FromQuery] int? serviceCategoryID,
                                                            [FromQuery] int? instrumentID,
                                                            [FromQuery] bool? status)
        {
            var merchantProducts = _merchantProductService.GetMerchantProducts(sendCountry, receiveCountry, sendCurrency, receiveCurrency, merchantID, merchantProductID, productID, serviceCategoryID, instrumentID, status);
            return Ok(merchantProducts);
        }
    }
}
