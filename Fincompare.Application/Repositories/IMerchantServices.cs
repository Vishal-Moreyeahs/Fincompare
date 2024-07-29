using Fincompare.Application.Request.MerchantRequests;
using Fincompare.Application.Response;

namespace Fincompare.Application.Repositories
{
    public interface IMerchantServices
    {
        Task<ApiResponse<MerchantDto>> AddMerchant(AddMerchantRequest model);
        Task<ApiResponse<MerchantDto>> EditMerchantProfile(UpdateMerchantRequest model);
        Task<ApiResponse<IEnumerable<MerchantDto>>> GetAllMerchants(int? groupMerchantId, int? merchantId, string? merchantType, string? couuntryIso3, bool? status);
        Task<ApiResponse<MerchantDto>> GetMerchantByMerchantId(int merchantId);
        Task<ApiResponse<MerchantDto>> GetMerchantByUserId(int userId);

        Task<ApiResponse<string>> DeleteMerchant(int merchantId);
    }
}
