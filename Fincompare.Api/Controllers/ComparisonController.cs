using Fincompare.Application.Repositories;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetComparisonResult(string sendCountry,
                                                            string receiveCountry,
                                                            string sendCurrency,
                                                            string receiveCurrency,
                                                            double sendAmount,
                                                            [FromQuery] int? productID,
                                                            [FromQuery] int? serviceCategoryID,
                                                            [FromQuery] int? instrumentID,
                                                            [FromQuery] bool? status)
        {
            var response = await _comparisonRateService.GetMerchantRatesFromTable(sendCountry,receiveCountry,sendCurrency,receiveCurrency,sendAmount,productID,serviceCategoryID,instrumentID);
            return Ok(response);
        }
    }
}
