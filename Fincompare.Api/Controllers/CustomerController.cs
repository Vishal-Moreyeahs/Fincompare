using Fincompare.Api.Middleware;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request;
using Fincompare.Application.Request.CustomerRequests;
using Fincompare.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerUserService _customerUserService;

        public CustomerController(ICustomerUserService customer)
        { 
            _customerUserService = customer;
        }

        [HttpPost]
        [ValidateModelState]
        [Route("add-customer")]
        public async Task<IActionResult> AddCustomerRecord(AddCustomerRequest model)
        {
            var response = await _customerUserService.AddCustomerAsUser(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("fetch-customers")]
        public async Task<IActionResult> GetCustomerRecord(int? customerId)
        {
            var response = await _customerUserService.GetCustomerAsUser(customerId);
            return Ok(response);
        }

        [HttpPut]
        [ValidateModelState]
        [Route("update-customer")]
        public async Task<IActionResult> UpdateCustomerRecord(UpdateCustomerRequest model)
        {
            var response = await _customerUserService.UpdateCustomerAsUser(model);

            return Ok(response);
        }
    }
}
