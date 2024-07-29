using Fincompare.Application.Request.ActiveAssetRequests;
using Fincompare.Application.Request.CityRequest;
using Fincompare.Application.Response;
using Fincompare.Application.Response.ActiveAssetResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Repositories
{
    public interface IActiveAssetService
    {
        Task<ApiResponse<ActiveAssetResponseViewModel>> AddActiveAssetMerchant(AddActiveAssetRequest model);
        Task<ApiResponse<ActiveAssetResponseViewModel>> UpdateActiveAssetMerchant(UpdateActiveAssetRequest model);
        Task<ApiResponse<IEnumerable<ActiveAssetResponseViewModel>>> GetAllActiveAssetRecord(string? countryIso3, int? assetMasterId, int? serviceCategoryId,int? merchantId ,DateTime? dateActive, DateTime? dateValidity ,bool? Status);
    }
}
