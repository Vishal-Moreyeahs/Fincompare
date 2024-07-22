using Fincompare.Application.Request.MarketRateRequest;
using Fincompare.Application.Response;
using Fincompare.Application.Response.MarketRateResponse;

namespace Fincompare.Application.Repositories
{
    public interface IMarketRateServices
    {
        Task<ApiResponse<string>> AddMarketRate(List<AddMarketRate> model);

        //Task<ApiResponse<string>> UpdateMarketRate(UpdateMarketRate model);

        Task<ApiResponse<IEnumerable<MarketRateDto>>> GetAllMarketRates();

        Task<ApiResponse<MarketRateDto>> GetMarketRateById(int id);
        Task<ApiResponse<MarketRateDto>> GetMarketRateBySourceAndDestCurr(string sourceCurr, string destCurr);

        Task<ApiResponse<List<MarketRateDto>>> GetMarketRateBySendCurr(string sendCurr);

        Task<ApiResponse<List<string>>> UpdateDbCurrencyExchangeRates();

        Task<MarketRateStatisticsData> GetAllMarketRatesStatistics(string sendCurr, string receiveCurr);
    }
}