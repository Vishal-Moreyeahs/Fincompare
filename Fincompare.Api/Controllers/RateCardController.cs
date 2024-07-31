using Fincompare.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateCardController : ControllerBase
    {
        private readonly IRateCardServices _rateCardService;

        public RateCardController(IRateCardServices rateCardServices)
        {
            _rateCardService = rateCardServices;
        }

        [HttpGet]
        [Route("fetch-rates-from-countryIso3")]
        public async Task<IActionResult> GetRatesFromSendCountry(string countryIso3)
        {
            var response = await _rateCardService.GetRateCardByCountry3Iso(countryIso3);
            return Ok(response);
        }
    }
}
