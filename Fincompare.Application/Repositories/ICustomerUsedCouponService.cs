using Fincompare.Application.Response;
using static Fincompare.Application.Request.CustomerUsedCouponRequest.CustomerUsedCouponViewRequest;
using static Fincompare.Application.Response.CustomerUsedCouponResponse.CustomerUsedCouponViewResponse;

namespace Fincompare.Application.Repositories
{
    public interface ICustomerUsedCouponService
    {
        Task<ApiResponse<string>> CustomerUsedCoupons(CreateCustomerUsedCouponRequest model);
        Task<ApiResponse<IEnumerable<GetAllCustomerUsedCouponResponse>>> GetAllCustomerUsedCoupons
            (
            int? merchantId,
            int? customerId,
            DateTime? startDateTime,
            DateTime? endDateTime,
            string? sendCountry,
            string? receiveCountry,
            string? sendCurrency,
            string? receiveCurrency,
            int? merchantProductID,
            int? serviceCategoryId,
            int? instrumentId,
            bool? isUsed
            );
    }
}
