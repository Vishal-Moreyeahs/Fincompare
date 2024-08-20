using Fincompare.Application.Request.CountryCurrencyRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.CountryCurrencyResponse;

namespace Fincompare.Application.Repositories
{
    public interface ICountryCurrencyManager
    {
        Task<ApiResponse<List<GetCountryCurrencyResponse>>> AddCountryWithMultipleCurrencies(UpdateCountryWithMultipleCurrencyRequest model);
        Task<ApiResponse<List<GetCountryCurrencyResponse>>> GetCurrenciesbyCountry3Iso(string? country3Iso, string? categoryId, string? currencyIso);
        Task<ApiResponse<GetCountryCurrencyResponse>> UpdateCountryCurrency(UpdateCountryCurrencyRequest model);

    }
}
