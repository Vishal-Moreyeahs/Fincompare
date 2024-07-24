using Fincompare.Api.Middleware;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request;
using Fincompare.Application.Request.ClickLeadRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerReviewController : ControllerBase
    {
        private readonly ICustomerReviewService _customerReviewService;

        public CustomerReviewController(ICustomerReviewService customerReviewService)
        {
            _customerReviewService = customerReviewService;
        }

        //[HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPost]
        [ValidateModelState]
        [Route("add-customer-review")]
        public async Task<IActionResult> AddCustomerReviewRecord(AddCustomerReviewRequest model)
        {
            var response = await _customerReviewService.AddCustomerReviewRecord(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("fetch-click-lead-record")]
        public async Task<IActionResult> GetCustomerReviewRecord(int? merchantId)
        {
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            var response = await _customerReviewService.GetAllResponse(merchantId);
            return Ok(response);
        }

        [HttpPut]
        [ValidateModelState]
        [Route("update-customer-review")]
        public async Task<IActionResult> UpdateCustomerReviewRecord(UpdateCustomerReviewRequest model)
        {
            var response = await _customerReviewService.UpdateCustomerReviewRecord(model);

            return Ok(response);
        }
    }
}
