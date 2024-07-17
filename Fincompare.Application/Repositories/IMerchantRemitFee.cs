using Fincompare.Application.Response;
using static Fincompare.Application.Request.MerchantRemitProductFeeRequests.MerchantRemitProductFeeRequestViewModel;
using static Fincompare.Application.Response.MerchantRemitFeeResponse.MerchantRemitFeeBaseResponse;

namespace Fincompare.Application.Repositories
{
    public interface IMerchantRemitFee
    {
        Task<ApiResponse<IEnumerable<MerchantRemittanceFee>>> AddMerchantRemitFee(CreateMerchantRemitProductFeeRequest model);
        Task<ApiResponse<IEnumerable<MerchantRemittanceFee>>> UpdateMerchantRemitFee(UpdateMerchantRemitProductFeeRequest model);
    }
}
