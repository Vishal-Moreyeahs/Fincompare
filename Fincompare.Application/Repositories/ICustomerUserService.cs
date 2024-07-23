using Fincompare.Application.Request.CustomerRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.CustomerUserResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Repositories
{
    public interface ICustomerUserService
    {
        Task<ApiResponse<CustomerUserResponseViewModel>> AddCustomerAsUser(AddCustomerRequest model);
        Task<ApiResponse<UpdateCustomerRequest>> UpdateCustomerAsUser(UpdateCustomerRequest model);
        Task<ApiResponse<IEnumerable<CustomerUserResponseViewModel>>> GetCustomerAsUser(int? customerId);
    }
}
