using Fincompare.Application.Response;
using static Fincompare.Application.Request.MerchantRemitProductFeeRequests.MerchantRemitProductFeeRequestViewModel;
using static Fincompare.Application.Response.MerchantRemitFeeResponse.MerchantRemitFeeBaseResponse;

namespace Fincompare.Application.Repositories
{
    public interface IMerchantRemitFee
    {
        Task<ApiResponse<MerchantRemittanceFee>> AddMerchantRemitFee(CreateMerchantRemitProductFeeRequest model);
        Task<ApiResponse<MerchantRemittanceFee>> UpdateMerchantRemitFee(UpdateMerchantRemitProductFeeRequest model);


        Task<ApiResponse<IEnumerable<MerchantRemittanceFee>>>
            GetMerchantRemittanceFee
            (
            string sendCountry,
            string receiveCountry,
            string sendCurrency,
            string receiveCurrency,
            int? merchantId,
            int? remittanceFeeId,
            int? merchantProductId,
            int? serviceCategoryId,
            int? instrumentId,
            double? sendMinLimit,
            double? receiveMinLimit,
            bool? status,
            bool? isValid);

        Task<ApiResponse<IEnumerable<MerchantRemittanceFee>>>
            GetMerchantRemittanceFeeByMerchant
            (
            int merchantId,
            string sendCountry,
            string receiveCountry,
            string sendCurrency,
            string receiveCurrency,
            int? remittanceFeeId,
            int? merchantProductId,
            int? serviceCategoryId,
            int? instrumentId,
            double? sendMinLimit,
            double? receiveMinLimit,
            bool? status,
            bool? isValid);
    }
}
