using Fincompare.Application.Repositories;
using Fincompare.Application.Request.CountryCurrencyRequests;
using Microsoft.AspNetCore.Mvc;

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
        [Route("add-currencies-for-country")]
        public async Task<IActionResult> AddCountryWithMultipleCurrencies(UpdateCountryWithMultipleCurrencyRequest model)
        {
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            var response = await _countryCurrencyManager.AddCountryWithMultipleCurrencies(model);
            return Ok(response);
        }
        
        [HttpPost]
        [Route("update-country-currency")]
        public async Task<IActionResult> UpdateCountryCurrency(UpdateCountryCurrencyRequest model)
        {
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            var response = await _countryCurrencyManager.UpdateCountryCurrency(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-currencies-by-country3iso")]
        public async Task<IActionResult> GetCurrenciesbyCountry(string country3iso, string? categoryId)
        {
            var response = await _countryCurrencyManager.GetCurrenciesbyCountry3Iso(country3iso, categoryId);
            return Ok(response);
        }
    }
}
