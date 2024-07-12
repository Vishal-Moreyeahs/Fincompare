using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using static Fincompare.Application.Request.CountryCurrencyRequest.CountryCurrencyBaseModel;

namespace Fincompare.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryCurrencyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICountryCurrencyService _countryCurrencyService;
        public CountryCurrencyController(IUnitOfWork unitOfWork, ICountryCurrencyService countryCurrencyService)
        {
            _unitOfWork = unitOfWork;
            _countryCurrencyService = countryCurrencyService;
        }

        [HttpPost]
        [Route("add-all-countrycurrency")]
        public async Task<IActionResult> AddCountryCurrency(AddCountryCurrencyRequest model)
        {
            var response = await _countryCurrencyService.AddCountryCurrency(model);
            return Ok(response);
        }

        [HttpPut]
        [Route("update-countrycurrency")]
        public async Task<IActionResult> UpdateCountryCurrency(UpdateCountryCurrency model)
        {
            var response = await _countryCurrencyService.UpdateCountryCurrency(model);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-all-countrycurrency")]
        public async Task<IActionResult> GetAllCountryCurrency()
        {
            var response = await _countryCurrencyService.GetAllCountryCurrency();
            return Ok(response);
        }

        [HttpGet]
        [Route("getby-id-countrycurrency")]
        public async Task<IActionResult> GetCountryCurrencyById(int id)
        {
            var response = await _countryCurrencyService.GetCountryCurrencyById(id);
            return Ok(response);
        }

        [HttpDelete]
        [Route("delete-countrycurrency")]
        public async Task<IActionResult> DeleteCountryCurrency(int id)
        {
            var response = await _countryCurrencyService.DeleteCountryCurrency(id);
            return Ok(response);
        }
    }
}
