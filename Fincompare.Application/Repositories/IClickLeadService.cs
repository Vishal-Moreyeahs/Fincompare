using Fincompare.Application.Request.ClickLeadRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.ClickLeadResponse;

namespace Fincompare.Application.Repositories
{
    public interface IClickLeadService
    {
        Task<ApiResponse<ClickLeadResponseViewModel>> AddClickLeadRedirections(AddClickLeadRequest model);
        Task<ApiResponse<IEnumerable<ClickLeadResponseViewModel>>> GetAllClickLeadRecords(int? merchantId, int? clickLeadId, int? customerId, string country3iso);
    }
}
