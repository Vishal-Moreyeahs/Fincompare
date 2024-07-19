using Fincompare.Application.Response;
using Fincompare.Domain.Entities;
using static Fincompare.Application.Request.CurrencyRequest.CurrencyRequests;
using static Fincompare.Application.Response.CurrencyResponse.CurrencyResponseBaseModel;

namespace Fincompare.Application.Repositories
{
    public interface ICurrencyServices
    {
        Task<ApiResponse<Currency>> AddCurrency(AddCurrencyRequests model);
        Task<ApiResponse<Currency>> UpdateCurrency(UpdateCurrencyRequests model);
        Task<ApiResponse<IEnumerable<GetAllCurrencyResponse>>> GetAllCurrency(string? country3Iso, string? currencyIso, bool? status);
        Task<ApiResponse<GetCurrencyResponse>> GetByCurrencyId(string id);
        Task<ApiResponse<string>> DeleteCurrency(string id);

    }
}
