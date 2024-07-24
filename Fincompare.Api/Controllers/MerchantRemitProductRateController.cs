using Fincompare.Application.Repositories;
using Fincompare.Application.Request.MerchantRemitProductRateRequests;
using Microsoft.AspNetCore.Mvc;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantRemitProductRateController : ControllerBase
    {
        private readonly IMerchantRemitProductRateService _merchantRemitProductRateService;

        public MerchantRemitProductRateController(IMerchantRemitProductRateService merchantRemitProductRateService)
        {
            _merchantRemitProductRateService = merchantRemitProductRateService;
        }

        [HttpPost]
        [Route("add-merchantremit-rate")]
        private async Task<IActionResult> AddMerchantRemitRate(AddMerchantRemitProductRateRequest model)
        {
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            var response = await _merchantRemitProductRateService.AddMerchantRemitProductRate(model);
            return Ok(response);
        }


        [HttpPut]
        [Route("fetch-merchant-rates-by-merchantid/{merchantId}")]
        private async Task<IActionResult> GetMerchantRateByMerchant(int merchantId)
        {
            var response = await _merchantRemitProductRateService.GetAllMerchantRemitProductRateByMerchant(merchantId);
            return Ok(response);
        }

        [HttpGet("Merchant-RemittanceRate/{sendCountry}/{receiveCountry}/{sendCurrency}/{receiveCurrency}")]
        private async Task<IActionResult> GetMerchantRemittanceFee(
            string sendCountry,
            string receiveCountry,
            string sendCurrency,
            string receiveCurrency,
            [FromQuery] int? merchantId,
            [FromQuery] int? remittanceRateId,
            [FromQuery] int? merchantProductId,
            [FromQuery] int? serviceCategoryId,
            [FromQuery] int? instrumentId,
            [FromQuery] int? sendMinLimit,
            [FromQuery] int? receiveMinLimit,
            [FromQuery] bool? status)
        {
            // Implement your logic here to process the request parameters
            // You can access and use these parameters as needed
            var response = await _merchantRemitProductRateService
                .GetAllMerchantRemitProductRate(
                sendCountry,
                receiveCountry,
                sendCurrency,
                receiveCurrency,
                merchantId,
                remittanceRateId,
                merchantProductId,
                serviceCategoryId,
                instrumentId,
                sendMinLimit,
                receiveMinLimit,
                status);
            return Ok(response);

        }


        [HttpGet("Merchant-RemittanceRate-MerchantId/{merchantId}/{sendCountry}/{receiveCountry}/{sendCurrency}/{receiveCurrency}")]
        private async Task<IActionResult> GetMerchantRemittanceRateByCurrencyPairAndMerchant(
            int merchantId,
            string sendCountry,
            string receiveCountry,
            string sendCurrency,
            string receiveCurrency,
            [FromQuery] int? remittanceRateId,
            [FromQuery] int? merchantProductId,
            [FromQuery] int? serviceCategoryId,
            [FromQuery] int? instrumentId,
            [FromQuery] int? sendMinLimit,
            [FromQuery] int? receiveMinLimit,
            [FromQuery] bool? status)
        {
            // Implement your logic here to process the request parameters
            // You can access and use these parameters as needed
            var response = await _merchantRemitProductRateService
                .GetAllMerchantRemitProductRateByCurrencyPairAndMerchant(
                sendCurrency,
                receiveCurrency,
                merchantId,
                remittanceRateId,
                merchantProductId,
                serviceCategoryId,
                instrumentId,
                sendMinLimit,
                receiveMinLimit,
                status);
            return Ok(response);

        }
    }
}
