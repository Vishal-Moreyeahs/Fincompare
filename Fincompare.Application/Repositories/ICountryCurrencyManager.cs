using Fincompare.Application.Request.CountryCurrencyRequests;
using Fincompare.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Repositories
{
    public interface ICountryCurrencyManager
    {
        Task<ApiResponse<string>> AddCountryWithCurrencies(AddCountryCurrencyRequest model);
    }
}
