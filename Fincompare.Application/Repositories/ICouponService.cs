using Fincompare.Application.Response;
using static Fincompare.Application.Request.CouponRequest.CouponRequestViewModel;
using static Fincompare.Application.Response.CouponResponse.CouponResponseViewModel;

namespace Fincompare.Application.Repositories
{
    public interface ICouponService
    {
        Task<ApiResponse<FetchCouponResponse>> CreateCoupon(CreateCouponRequest model);
        Task<ApiResponse<FetchCouponResponse>> UpdateCoupon(UpdateCouponRequest model);
        Task<ApiResponse<IEnumerable<FetchCouponResponse>>> GetAllCoupon(int? couponId, bool? status);
    }
}
