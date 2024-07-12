using Fincompare.Application.Repositories;
using Fincompare.Application.Request.CountryCurrencyRequests;
using Fincompare.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Fincompare.Application.Request.CurrencyRequest.CurrencyRequests;

namespace Fincompare.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryCurrencyController : ControllerBase
    {
        private readonly ICountryCurrencyManager _countryCurrencyManager;

        public CountryCurrencyController(ICountryCurrencyManager countryCurrencyManager)
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

        [HttpGet]
        [Route("get-currencies-by-country3iso")]
        public async Task<IActionResult> GetCurrenciesbyCountry(string country3iso)
        {
            var response = await _countryCurrencyManager.GetCurrenciesbyCountry3Iso(country3iso);
            return Ok(response);
        }
    }
}
