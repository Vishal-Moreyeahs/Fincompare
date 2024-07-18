using Fincompare.Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using static Fincompare.Application.Request.MerchantRemitProductFeeRequests.MerchantRemitProductFeeRequestViewModel;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantRemitFeeController : ControllerBase
    {
        private readonly IMerchantRemitFee _merchantRemitFee;

        public MerchantRemitFeeController(IMerchantRemitFee merchantRemitFee)
        {
            _merchantRemitFee = merchantRemitFee;
        }

        [HttpPost]
        [Route("add-merchantremit-fee")]
        public async Task<IActionResult> AddMerchantRemitFee(CreateMerchantRemitProductFeeRequest model)
        {
            var response = await _merchantRemitFee.AddMerchantRemitFee(model);
            return Ok(response);
        }


        [HttpPost]
        [Route("update-merchantremit-fee")]
        public async Task<IActionResult> UpdateMerchantRemitFee(UpdateMerchantRemitProductFeeRequest model)
        {
            var response = await _merchantRemitFee.UpdateMerchantRemitFee(model);
            return Ok(response);
        }

        [HttpGet("Merchant-RemittanceFee/{sendCountry}/{receiveCountry}/{sendCurrency}/{receiveCurrency}")]
        public IActionResult GetMerchantRemittanceFee(
            string sendCountry,
            string receiveCountry,
            string sendCurrency,
            string receiveCurrency,
            [FromQuery] string? merchantId,
            [FromQuery] string? remittanceFeeId,
            [FromQuery] string? merchantProductId,
            [FromQuery] string? serviceCategoryId,
            [FromQuery] string? instrumentId,
            [FromQuery] string? sendMinLimit,
            [FromQuery] string? receiveMinLimit,
            [FromQuery] bool? status,
            [FromQuery] bool? isValid)
        {
            // Implement your logic here to process the request parameters
            // You can access and use these parameters as needed

            

            return Ok("response");
        }
    }
}
