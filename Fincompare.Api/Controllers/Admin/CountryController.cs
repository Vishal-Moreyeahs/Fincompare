using Fincompare.Application.Repositories;
using Fincompare.Application.Request.CountryRequest;
using Microsoft.AspNetCore.Mvc;

namespace Fincompare.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryServices _countryServices;

        public CountryController(ICountryServices countryServices)
        {
            _countryServices = countryServices;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateCompany(CountryRequest request)
        {
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            return Ok(await _countryServices.AddCountry(request));
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateCompany(CountryRequest request)
        {
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            return Ok(await _countryServices.UpdateCountry(request));
        }

        [HttpDelete]
        [Route("remove")]
        public async Task<IActionResult> DeleteCompanyById(string country3iso)
        {
            return Ok(await _countryServices.RemoveCountry(country3iso));
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllCompany()
        {
            return Ok(await _countryServices.GetAllCountry());
        }

        [HttpGet]
        [Route("get-by-country3Iso")]
        public async Task<IActionResult> GetCompanyById(string country3iso)
        {
            return Ok(await _countryServices.GetCountryByCountryName(country3iso));
        }
    }
}
