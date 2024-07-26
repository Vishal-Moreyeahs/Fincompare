using Fincompare.Application.Request;
using Fincompare.Application.Response;
using Fincompare.Application.Response.CustomerReviewResponse;

namespace Fincompare.Application.Repositories
{
    public interface ICustomerReviewService
    {
        Task<ApiResponse<CustomerReviewResponseViewModel>> AddCustomerReviewRecord(AddCustomerReviewRequest model);
        Task<ApiResponse<CustomerReviewResponseViewModel>> UpdateCustomerReviewRecord(UpdateCustomerReviewRequest model);
        Task<ApiResponse<IEnumerable<CustomerReviewResponseViewModel>>> GetAllResponse(int? merchantId);
    }
}
