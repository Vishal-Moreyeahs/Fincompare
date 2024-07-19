using Fincompare.Application.Request.MerchantRemitProductRateRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.MerchantRemitProductRateResponse;

namespace Fincompare.Application.Repositories
{
    public interface IMerchantRemitProductRateService
    {
        Task<ApiResponse<MerchantRemitProductRateViewModel>> AddMerchantRemitProductRate(AddMerchantRemitProductRateRequest model);
    }
}
