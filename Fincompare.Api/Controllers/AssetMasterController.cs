using Fincompare.Api.Middleware;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.ActiveAssetRequests;
using Fincompare.Domain.Enums;
using Fincompare.Infrastructure.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetMasterController : ControllerBase
    {
        private readonly IAssetMasterService _assetMasterService;

        public AssetMasterController(IAssetMasterService assetMasterService)
        {
            _assetMasterService = assetMasterService;
        }

        [HttpGet]
        [Route("get-assets-master-record")]
        public async Task<IActionResult> AddActiveAsset(string? countryIso3, int? assetMasterId, bool? status)
        {
            var response = await _assetMasterService.GetAllAssetMaster(countryIso3, assetMasterId, status);
            return Ok(response);
        }

    }
}
