using Fincompare.Application.Request.CountryCurrencyRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.CountryCurrencyResponse;
using static Fincompare.Application.Response.CurrencyResponse.CurrencyResponseBaseModel;

namespace Fincompare.Application.Repositories
{
    public interface ICountryCurrencyManager
    {
        Task<ApiResponse<string>> UpdateCountryWithMultipleCurrencies(UpdateCountryWithMultipleCurrencyRequest model);
        Task<ApiResponse<List<GetCountryCurrencyResponse>>> GetCurrenciesbyCountry3Iso(string country3Iso, string? categoryId);

    }
}
