using Fincompare.Application.Repositories;
using Fincompare.Application.Request.CustomerRequests;
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
        [Route("add-customer")]
        public async Task<IActionResult> AddCustomerRecord(AddCustomerRequest model)
        {
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            var response = await _customerUserService.AddCustomerAsUser(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-customers")]
        public async Task<IActionResult> GetCustomerRecord(int? customerId)
        {
            var response = await _customerUserService.GetCustomerAsUser(customerId);
            return Ok(response);
        }

        [HttpPut]
        [Route("update-customer")]
        public async Task<IActionResult> UpdateCustomerRecord(UpdateCustomerRequest model)
        {
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            var response = await _customerUserService.UpdateCustomerAsUser(model);

            return Ok(response);
        }
    }
}
