using Fincompare.Api.Middleware;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.ClickLeadRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionController : ControllerBase
    {
        private readonly IConversionService _conversionService;

        public ConversionController(IConversionService conversionService)
        { 
            _conversionService = conversionService;
        }
        
        /// <summary>
        /// This Api for conversion
        /// </summary>
        /// <param name="sendCurrency"></param>
        /// <param name="receiveCurrency"></param>
        /// <param name="sendAmount"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{sendCurrency}/{receiveCurrency}/{sendAmount}")]
        public async Task<IActionResult> GetConversionRate(string sendCurrency, string receiveCurrency, double sendAmount)
        {
            var response = await _conversionService.GetConversionResult(sendCurrency,receiveCurrency,sendAmount);
            return Ok(response);
        }
    }
}
