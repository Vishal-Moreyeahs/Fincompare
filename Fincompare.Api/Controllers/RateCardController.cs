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
        [Route("fetch-rates-from-country3iso")]
        public async Task<IActionResult> GetRatesFromSendCountry(string country3iso)
        {
            var response = await _rateCardService.GetRateCardByCountry3Iso(country3iso);
            return Ok(response);
        }
    }
}
