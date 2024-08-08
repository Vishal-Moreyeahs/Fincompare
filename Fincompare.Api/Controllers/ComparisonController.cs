using Fincompare.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComparisonController : ControllerBase
    {
        private readonly IComparisonRateService _comparisonRateService;

        public ComparisonController(IComparisonRateService comparisonRateService)
        {
            _comparisonRateService = comparisonRateService;
        }

        [HttpGet]
        [Route("{sendCountry}/{receiveCountry}/{sendCurrency}/{receiveCurrency}/{sendAmount}")]
        public async Task<IActionResult> GetMerchantRatesFromTable(
                    string sendCountry,
                    string receiveCountry,
                    string sendCurrency,
                    string receiveCurrency,
                    double sendAmount,/* double receiveAmount,*/
                    int? productId,
                    int? serviceCategoryId,
                    int? instrumentId)
        {
            var response = await _comparisonRateService.GetMerchantRatesFromTable(sendCountry, receiveCountry, sendCurrency, receiveCurrency, sendAmount, productId, serviceCategoryId, instrumentId);
            return Ok(response);
        }
    }
}

