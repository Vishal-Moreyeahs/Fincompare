using Fincompare.Application.Response;
using static Fincompare.Application.Request.GroupMerchantRequest.GroupMerchantBaseModel;
using static Fincompare.Application.Response.GroupMerchantResponse.GroupMerchantViewResponse;

namespace Fincompare.Application.Repositories
{
    public interface IGroupMerchantService
    {
        Task<ApiResponse<IEnumerable<GetAllGroupMerchantResponse>>> AddGroupMerchant(AddGroupMerchantRequestClass model);
        Task<ApiResponse<IEnumerable<UpdateGroupMerchantRequestClass>>> UpdateGroupMerchant(UpdateGroupMerchantRequestClass model);
        Task<ApiResponse<IEnumerable<GetAllGroupMerchantResponse>>> GetAllGroupMerchant(int? groupMerchantId, string? countryIso3, bool? status);
        Task<ApiResponse<GetAllGroupMerchantResponse>> GetByIdGroupMerchant(int id);
    }
}
