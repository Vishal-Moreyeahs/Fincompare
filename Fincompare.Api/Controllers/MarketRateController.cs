using Fincompare.Application.Repositories;
using Fincompare.Application.Request.MarketRateRequest;
using Microsoft.AspNetCore.Mvc;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketRateController : ControllerBase
    {
        private readonly IMarketRateServices _marketRateServices;

        public MarketRateController(IMarketRateServices marketRateServices)
        {
            _marketRateServices = marketRateServices;
        }


        [HttpPost]
        [Route("add-market-rate")]
        public async Task<IActionResult> AddMarketRate(AddMarketRate model)
        {
            var response = await _marketRateServices.AddMarketRate(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-all-market-rate")]
        public async Task<IActionResult> GetAllMarketRates()
        {
            var response = await _marketRateServices.GetAllMarketRates();
            return Ok(response);
        }

        [HttpGet]
        [Route("get-market-rate-by-id")]
        public async Task<IActionResult> GetMarketRatesById(int marketRateId)
        {
            var response = await _marketRateServices.GetMarketRateById(marketRateId);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-market-rate-by-sendCurr")]
        public async Task<IActionResult> GetMarketRatesBySourceCurrency(string sendCurr)
        {
            var response = await _marketRateServices.GetMarketRateBySendCurr(sendCurr);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-market-rate-by-sendAndReceiveCurr")]
        public async Task<IActionResult> GetMarketRateBySourceAndDestCurr(string sendCurr, string receiveCurr)
        {
            var response = await _marketRateServices.GetMarketRateBySourceAndDestCurr(sendCurr, receiveCurr);
            return Ok(response);
        }

        //[HttpGet]
        //[Route("get-market-rate-api")]
        //private async Task<IActionResult> UpdateDbCurrencyExchangeRates()
        //{
        //    var response = await _marketRateServices.UpdateDbCurrencyExchangeRates();
        //    return Ok(response);
        //}
    }
}

