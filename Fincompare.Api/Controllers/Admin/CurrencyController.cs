using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using static Fincompare.Application.Request.CurrencyRequest.CurrencyRequests;
namespace Fincompare.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrencyServices _currencyServices;
        public CurrencyController(IUnitOfWork unitOfWork, ICurrencyServices currencyServices)
        {
            _unitOfWork = unitOfWork;
            _currencyServices = currencyServices;
        }


        [HttpPost]
        [Route("add-all-currency")]
        public async Task<IActionResult> AddCurrency(AddCurrencyRequests model)
        {
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            var response = await _currencyServices.AddCurrency(model);
            return Ok(response);
        }


        [HttpPut]
        [Route("update-currency")]
        public async Task<IActionResult> UpdateCurrency(UpdateCurrencyRequests model)
        {
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            var response = await _currencyServices.UpdateCurrency(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-all-currency")]
        public async Task<IActionResult> GetAllCurrency(string? country3Iso, string? currencyIso, bool? status)
        {
            var response = await _currencyServices.GetAllCurrency(country3Iso, currencyIso, status);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-by-currencyid")]
        public async Task<IActionResult> GetByCurrencyId(string currency3Iso)
        {
            var response = await _currencyServices.GetByCurrencyId(currency3Iso);
            return Ok(response);
        }

        [HttpDelete]
        [Route("delete-currency")]
        public async Task<IActionResult> DeleteCurrency(string currency3Iso)
        {
            var response = await _currencyServices.DeleteCurrency(currency3Iso);
            return Ok(response);
        }
    }
}
