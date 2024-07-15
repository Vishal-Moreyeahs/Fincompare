using Fincompare.Application.Request.MerchantRequests;
using Fincompare.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Repositories
{
    public interface IMerchantServices
    {
        Task<ApiResponse<string>> OnboardMerchant(AddMerchantRequest model);
        Task<ApiResponse<string>> EditMerchantProfile(UpdateMerchantRequest model);
        Task<ApiResponse<IEnumerable<MerchantDto>>> GetAllMerchants();
        Task<ApiResponse<MerchantDto>> GetMerchantByMerchantId(string merchantId);
        Task<ApiResponse<MerchantDto>> GetMerchantByUserId(int userId);

        Task<ApiResponse<string>> DeleteMerchant(int merchantId);
    }
}
