using Fincompare.Application.Repositories;
using Fincompare.Domain.Enums;
using Fincompare.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;
using static Fincompare.Application.Request.GroupMerchantRequest.GroupMerchantBaseModel;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMerchantController : ControllerBase
    {
        private readonly IGroupMerchantService _groupMerchantService;

        public GroupMerchantController(IGroupMerchantService groupMerchantService)
        {
            _groupMerchantService = groupMerchantService;
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPost]
        [Route("add-group-merchant")]
        public async Task<IActionResult> AddGroupMerchant(AddGroupMerchantRequestClass model)
        {
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            var response = await _groupMerchantService.AddGroupMerchant(model);
            return Ok(response);
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPut]
        [Route("update-group-merchant")]
        public async Task<IActionResult> UpdateGroupMerchant(UpdateGroupMerchantRequestClass model)
        {
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            var response = await _groupMerchantService.UpdateGroupMerchant(model);
            return Ok(response);
        }


        //[HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpGet]
        [Route("getall-group-merchant")]
        public async Task<IActionResult> GetAllGroupMerchant(int? groupMerchantId, string? countryIso3, bool? status)
        {
            var response = await _groupMerchantService.GetAllGroupMerchant(groupMerchantId, countryIso3, status);
            return Ok(response);
        }

        //[HasPermission(PermissionEnum.CanAccessMerchant)]
        //[HttpGet]
        //[Route("getby-id-merchant")]
        //public async Task<IActionResult> GetByIdGroupMerchant(int id)
        //{
        //    var response = await _groupMerchantService.GetByIdGroupMerchant(id);
        //    return Ok(response);
        //}


    }
}
