using Fincompare.Application.Repositories;
using Microsoft.AspNetCore.Http;
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
            var merchantProducts = await _merchantProductService.GetMerchantProducts(sendCountry, receiveCountry, sendCurrency, receiveCurrency, merchantID, merchantProductID, productID, serviceCategoryID, instrumentID, status);
            return Ok(merchantProducts);
        }
    }
}
