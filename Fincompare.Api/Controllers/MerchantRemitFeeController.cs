using Fincompare.Api.Middleware;
using Fincompare.Application.Repositories;
using Fincompare.Domain.Enums;
using Fincompare.Infrastructure.Authentication;
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

        [HasPermission(PermissionEnum.CanAccessMerchant)]
        [HttpPost]
        [ValidateModelState]
        [Route("add-merchantremit-fee")]
        public async Task<IActionResult> AddMerchantRemitFee(CreateMerchantRemitProductFeeRequest model)
        {
            var response = await _merchantRemitFee.AddMerchantRemitFee(model);
            return Ok(response);
        }


        [HttpPut]
        [ValidateModelState]
        [Route("update-merchantremit-fee")]
        public async Task<IActionResult> UpdateMerchantRemitFee(UpdateMerchantRemitProductFeeRequest model)
        {
            var response = await _merchantRemitFee.UpdateMerchantRemitFee(model);
            return Ok(response);
        }

        [HttpGet("Merchant-RemittanceFee/{sendCountry}/{receiveCountry}/{sendCurrency}/{receiveCurrency}")]
        public async Task<IActionResult> GetMerchantRemittanceFee(
            string sendCountry,
            string receiveCountry,
            string sendCurrency,
            string receiveCurrency,
            [FromQuery] int? merchantId,
            [FromQuery] int? remittanceFeeId,
            [FromQuery] int? merchantProductId,
            [FromQuery] int? serviceCategoryId,
            [FromQuery] int? instrumentId,
            [FromQuery] int? sendMinLimit,
            [FromQuery] int? receiveMinLimit,
            [FromQuery] bool? status,
            [FromQuery] bool? isValid)
        {
            // Implement your logic here to process the request parameters
            // You can access and use these parameters as needed
            var response = await _merchantRemitFee
                .GetMerchantRemittanceFee(
                sendCountry,
                receiveCountry,
                sendCurrency,
                receiveCurrency,
                merchantId,
                remittanceFeeId,
                merchantProductId,
                serviceCategoryId,
                instrumentId,
                sendMinLimit,
                receiveMinLimit,
                status,
                isValid);
            return Ok(response);

        }


        [HttpGet("Merchant-RemittanceFee-MerchantId/{merchantId}/{sendCountry}/{receiveCountry}/{sendCurrency}/{receiveCurrency}")]
        public async Task<IActionResult> GetMerchantRemittanceFeeByMerchant(
            int merchantId,
            string sendCountry,
            string receiveCountry,
            string sendCurrency,
            string receiveCurrency,
            [FromQuery] int? remittanceFeeId,
            [FromQuery] int? merchantProductId,
            [FromQuery] int? serviceCategoryId,
            [FromQuery] int? instrumentId,
            [FromQuery] int? sendMinLimit,
            [FromQuery] int? receiveMinLimit,
            [FromQuery] bool? status,
            [FromQuery] bool? isValid)
        {
            // Implement your logic here to process the request parameters
            // You can access and use these parameters as needed
            var response = await _merchantRemitFee
                .GetMerchantRemittanceFeeByMerchant(
                merchantId,
                sendCountry,
                receiveCountry,
                sendCurrency,
                receiveCurrency,
                remittanceFeeId,
                merchantProductId,
                serviceCategoryId,
                instrumentId,
                sendMinLimit,
                receiveMinLimit,
                status,
                isValid);
            return Ok(response);

        }
    }
}
