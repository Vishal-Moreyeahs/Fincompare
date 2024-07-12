using Fincompare.Application.Repositories;
using Fincompare.Application.Request.CountryCurrencyRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketRateController : ControllerBase
    {
        private readonly ICountryCurrencyManager _countryCurrencyManager;

        public MarketRateController(ICountryCurrencyManager countryCurrencyManager)
        {
            _countryCurrencyManager = countryCurrencyManager;
        }

        [HttpPost]
        [Route("update-currencies-for-country")]
        public async Task<IActionResult> UpdateCountryWithMultipleCurrencies(UpdateCountryWithMultipleCurrencyRequest model)
        {
            var response = await _countryCurrencyManager.UpdateCountryWithMultipleCurrencies(model);
            return Ok(response);
        }
    }
}
