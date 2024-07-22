using Fincompare.Application.Repositories;
using Fincompare.Application.Request.MerchantRequests;
using Fincompare.Application.Services;
using Microsoft.AspNetCore.Http;
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
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            var response = await _rateCardService.GetRateCardByCountry3Iso(country3iso);
            return Ok(response);
        }
    }
}
