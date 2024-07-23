using Fincompare.Application.Request;
using Fincompare.Application.Response;
using Fincompare.Application.Response.CustomerReviewResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Repositories
{
    public interface ICustomerReviewService
    {
        Task<ApiResponse<CustomerReviewResponseViewModel>> AddCustomerReviewRecord(AddCustomerReviewRequest model);
        Task<ApiResponse<CustomerReviewResponseViewModel>> UpdateCustomerReviewRecord(UpdateCustomerReviewRequest model);
        Task<ApiResponse<IEnumerable<CustomerReviewResponseViewModel>>> GetAllResponse(int? merchantId);
    }
}
