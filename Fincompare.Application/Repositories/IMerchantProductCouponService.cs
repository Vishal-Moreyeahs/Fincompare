using Fincompare.Application.Response;
using static Fincompare.Application.Request.MerchantProductCouponRequest.MerchantProductRequestViewModel;
using static Fincompare.Application.Response.MerchantProductCouponResponse.MerchantProductCouponViewResponse;

namespace Fincompare.Application.Repositories
{
    public interface IMerchantProductCouponService
    {
        Task<ApiResponse<IEnumerable<GetAllMerchantProductCouponResponse>>> CreateMerchantProductCoupons(List<CreateMerchantProductCouponRequest> model);

        Task<ApiResponse<IEnumerable<MerchantCouponResponseClass>>> GetAllMerchantProductCoupons
            (
            int merchantId,
            int? merchantProductId,
            int? serviceCategoryId,
            int? instrumentId,
            int? productId,
            string? sendCountry,
            string? receiveCountry,
            string? sendCurrency,
            string? receiveCurrency,
            bool? IsUsed,
            bool? status
            );
    }
}
