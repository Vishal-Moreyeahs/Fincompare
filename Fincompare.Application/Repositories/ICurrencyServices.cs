using Fincompare.Application.Response;
using static Fincompare.Application.Request.CurrencyRequest.CurrencyRequests;
using static Fincompare.Application.Response.CurrencyResponse.CurrencyResponseBaseModel;

namespace Fincompare.Application.Repositories
{
    public interface ICurrencyServices
    {
        Task<ApiResponse<string>> AddCurrency(AddCurrencyRequests model);
        Task<ApiResponse<string>> UpdateCurrency(UpdateCurrencyRequests model);
        Task<ApiResponse<IEnumerable<GetAllCurrencyResponse>>> GetAllCurrency();
        Task<ApiResponse<GetCurrencyResponse>> GetByCurrencyId(int id);
        Task<ApiResponse<string>> DeleteCurrency(int id);

    }
}
