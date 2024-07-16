using Fincompare.Application.Models.RateModel;
using Fincompare.Application.Response;

namespace Fincompare.Application.Contracts.Infrastructure
{
    public interface IExchangeRate
    {
        API_Obj Import(string baseCur);
        Task<ApiResponse<List<string>>> UpdateDbCurrencyExchangeRates();
    }
}
