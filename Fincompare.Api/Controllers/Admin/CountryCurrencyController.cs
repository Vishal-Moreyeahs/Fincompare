using Fincompare.Api.Middleware;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.CountryCurrencyRequests;
using Fincompare.Domain.Enums;
using Fincompare.Infrastructure.Authentication;
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

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [ValidateModelState]
        [HttpPost]
        [Route("add-currencies-for-country")]
        public async Task<IActionResult> AddCountryWithMultipleCurrencies(UpdateCountryWithMultipleCurrencyRequest model)
        {
            var response = await _countryCurrencyManager.AddCountryWithMultipleCurrencies(model);
            return Ok(response);
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [ValidateModelState]
        [HttpPut]
        [Route("update-country-currency")]
        public async Task<IActionResult> UpdateCountryCurrency(UpdateCountryCurrencyRequest model)
        {
            var response = await _countryCurrencyManager.UpdateCountryCurrency(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("fetch-currencies-by-countryIso3")]
        public async Task<IActionResult> GetCurrenciesbyCountry(string? countryIso3, string? categoryId)
        {
            var response = await _countryCurrencyManager.GetCurrenciesbyCountry3Iso(countryIso3, categoryId);
            return Ok(response);
        }
    }
}
