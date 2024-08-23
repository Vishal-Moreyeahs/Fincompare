using Fincompare.Application.Response;
using Fincompare.Application.Response.AssetMasterResponse;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Repositories
{
    public interface IAssetMasterService
    {
        Task<ApiResponse<IEnumerable<AssetMasterViewModel>>> GetAllAssetMaster(string? countryIso3, int? assetMasterId ,bool? status);
    } 
}
