using Fincompare.Application.Request.CountryCurrencyRequests;
using Fincompare.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fincompare.Application.Response.CurrencyResponse.CurrencyResponseBaseModel;

namespace Fincompare.Application.Repositories
{
    public interface ICountryCurrencyManager
    {
        Task<ApiResponse<string>> UpdateCountryWithMultipleCurrencies(UpdateCountryWithMultipleCurrencyRequest model);
        Task<ApiResponse<List<GetCurrencyResponse>>> GetCurrenciesbyCountry3Iso(string country3Iso, int? categoryId);

    }
}
