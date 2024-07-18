using Fincompare.Application.Repositories;
using Fincompare.Application.Request.MarketRateRequest;
using Fincompare.Application.Request.MerchantRequests;
using Fincompare.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

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


        [HttpPost]
        [Route("onboard-merchant")]
        public async Task<IActionResult> OnBoardMerchant(AddMerchantRequest model)
        {
            var response = await _merchantServices.AddMerchant(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-all-merchants")]
        public async Task<IActionResult> GetAllMerchants(int? groupMerchantId, int? merchantId, string? couuntryIso3, bool? status )
        {
            var response = await _merchantServices.GetAllMerchants(groupMerchantId,  merchantId,  couuntryIso3,  status);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-market-rate-by-id")]
        public async Task<IActionResult> GetMerchantByMerchantId(int merchantId)
        {
            var response = await _merchantServices.GetMerchantByMerchantId(merchantId);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-merchant-by-userId")]
        public async Task<IActionResult> GetMerchantByUserId(int userId)
        {
            var response = await _merchantServices.GetMerchantByUserId(userId);
            return Ok(response);
        }

        [HttpDelete]
        [Route("delete-merchant-by-id")]
        public async Task<IActionResult> DeleteMerchant(int merchantId)
        {
            var response = await _merchantServices.DeleteMerchant(merchantId);
            return Ok(response);
        }

        [HttpPut]
        [Route("update-merchant")]
        public async Task<IActionResult> UpdateMerchant(UpdateMerchantRequest model)
        {
            var response = await _merchantServices.EditMerchantProfile(model);
            return Ok(response);
        }
    }
}
