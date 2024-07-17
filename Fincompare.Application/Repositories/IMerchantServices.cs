using Fincompare.Application.Request.MerchantRequests;
using Fincompare.Application.Response;

namespace Fincompare.Application.Repositories
{
    public interface IMerchantServices
    {
        Task<ApiResponse<MerchantDto>> OnboardMerchant(AddMerchantRequest model);
        Task<ApiResponse<MerchantDto>> EditMerchantProfile(UpdateMerchantRequest model);
        Task<ApiResponse<IEnumerable<MerchantDto>>> GetAllMerchants();
        Task<ApiResponse<MerchantDto>> GetMerchantByMerchantId(int merchantId);
        Task<ApiResponse<MerchantDto>> GetMerchantByUserId(int userId);

        Task<ApiResponse<string>> DeleteMerchant(int merchantId);
    }
}
