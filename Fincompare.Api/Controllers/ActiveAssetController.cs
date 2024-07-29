using Fincompare.Api.Middleware;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.ActiveAssetRequests;
using Fincompare.Application.Request.ClickLeadRequests;
using Fincompare.Domain.Enums;
using Fincompare.Infrastructure.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActiveAssetController : ControllerBase
    {
        private readonly IActiveAssetService _activeAssetService;

        public ActiveAssetController(IActiveAssetService activeAssetService)
        {
            _activeAssetService = activeAssetService;
        }

        [HasPermission(PermissionEnum.CanAccessMerchant)]
        [HttpPost]
        [ValidateModelState]
        [Route("add-active-asset")]
        public async Task<IActionResult> AddActiveAsset(AddActiveAssetRequest model)
        {
            var response = await _activeAssetService.AddActiveAssetMerchant(model);
            return Ok(response);
        }

        [HasPermission(PermissionEnum.CanAccessMerchant)]
        [HttpPut]
        [ValidateModelState]
        [Route("update-active-asset")]
        public async Task<IActionResult> UpdateActiveAsset(UpdateActiveAssetRequest model)
        {
            var response = await _activeAssetService.UpdateActiveAssetMerchant(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("fetch-active-asset-record")]
        public async Task<IActionResult> FetchActiveAssetRecords(string? country3iso, int? assetMasterId, int? serviceCategoryId, int? merchantId, DateTime? dateActive, DateTime? dateValidity, bool? status)
        {
            var response = await _activeAssetService.GetAllActiveAssetRecord(country3iso, assetMasterId,serviceCategoryId,merchantId,dateActive,dateValidity,status);
            return Ok(response);
        }
    }
}
