using Fincompare.Application.Repositories;
using Fincompare.Application.Request.ClickLeadRequests;
using Fincompare.Application.Services;
using Fincompare.Domain.Enums;
using Fincompare.Infrastructure.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Fincompare.Application.Request.GroupMerchantRequest.GroupMerchantBaseModel;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClickLeadController : ControllerBase
    {
        private readonly IClickLeadService _clickLeadService;

        public ClickLeadController(IClickLeadService clickLeadService)
        { 
            _clickLeadService = clickLeadService;
        }

        //[HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPost]
        [Route("add-click-lead-record")]
        public async Task<IActionResult> AddClickLeadRecord(AddClickLeadRequest model)
        {
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            var response = await _clickLeadService.AddClickLeadRedirections(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-click-lead-record")]
        public async Task<IActionResult> GetClickLeadRecord(int? merchantId, int? clickLeadId, int? customerId, string? country3iso)
        {
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            var response = await _clickLeadService.GetAllClickLeadRecords(merchantId,clickLeadId,customerId,country3iso);
            return Ok(response);
        }
    }
}
