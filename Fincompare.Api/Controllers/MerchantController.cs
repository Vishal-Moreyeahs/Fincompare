﻿using Fincompare.Api.Middleware;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.MerchantRequests;
using Fincompare.Domain.Enums;
using Fincompare.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantServices _merchantServices;

        public MerchantController(IMerchantServices merchantServices)
        {
            _merchantServices = merchantServices;
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPost]
        [ValidateModelState]
        [Route("add-merchant")]
        public async Task<IActionResult> AddMerchant(AddMerchantRequest model)
        {
            var response = await _merchantServices.AddMerchant(model);
            return Ok(response);
        }

        //[HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpGet]
        [Route("fetch-all-merchants")]
        public async Task<IActionResult> GetAllMerchants(int? groupMerchantId, int? merchantId, string? merchantType, string? couuntryIso3, bool? status)
        {
            var response = await _merchantServices.GetAllMerchants(groupMerchantId, merchantId, merchantType, couuntryIso3, status);
            return Ok(response);
        }


        //[HttpGet]
        //[Route("fetch-merchant-by-merchantid")]
        //public async Task<IActionResult> GetMerchantByMerchantId(int merchantId)
        //{
        //    var response = await _merchantServices.GetMerchantByMerchantId(merchantId);
        //    return Ok(response);
        //}


        [HttpGet]
        [Route("fetch-merchant-by-userId")]
        public async Task<IActionResult> GetMerchantByUserId(int userId)
        {
            var response = await _merchantServices.GetMerchantByUserId(userId);
            return Ok(response);
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpDelete]
        [Route("delete-merchant-by-id")]
        public async Task<IActionResult> DeleteMerchant(int merchantId)
        {
            var response = await _merchantServices.DeleteMerchant(merchantId);
            return Ok(response);
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPut]
        [ValidateModelState]
        [Route("update-merchant")]
        public async Task<IActionResult> UpdateMerchant(UpdateMerchantRequest model)
        {
            var response = await _merchantServices.EditMerchantProfile(model);
            return Ok(response);
        }
    }
}
