using Fincompare.Application.Request.CountryCurrencyRequests;
using Fincompare.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fincompare.Application.Request.GroupMerchantRequest.GroupMerchantBaseModel;
using static Fincompare.Application.Response.GroupMerchantResponse.GroupMerchantViewResponse;

namespace Fincompare.Application.Repositories
{
    public interface IGroupMerchantService
    {
        Task<ApiResponse<string>> AddGroupMerchant(AddGroupMerchantRequestClass model);
        Task<ApiResponse<string>> UpdateGroupMerchant(UpdateGroupMerchantRequestClass model);
        Task<ApiResponse<IEnumerable<GetAllGroupMerchantResponse>>> GetAllGroupMerchant();
        Task<ApiResponse<GetAllGroupMerchantResponse>> GetByIdGroupMerchant(int id);
    }
}
