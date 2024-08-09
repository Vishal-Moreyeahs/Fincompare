using Fincompare.Application.Request.ActiveAssetRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.ActiveAssetResponse;

namespace Fincompare.Application.Repositories
{
    public interface IActiveAssetService
    {
        Task<ApiResponse<ActiveAssetResponseViewModel>> AddActiveAssetMerchant(AddActiveAssetRequest model);
        Task<ApiResponse<ActiveAssetResponseViewModel>> UpdateActiveAssetMerchant(UpdateActiveAssetRequest model);
        Task<ApiResponse<IEnumerable<ActiveAssetResponseViewModel>>> GetAllActiveAssetRecord(int? assetMasterId, int? merchantId, bool? status);
    }
}
