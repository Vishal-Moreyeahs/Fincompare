using Fincompare.Application.Request.MarketRateRequest;
using Fincompare.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Repositories
{
    public interface IMarketRateServices
    {
        Task<ApiResponse<string>> AddMarketRate(AddMarketRate model);

        Task<ApiResponse<string>> UpdateMarketRate(UpdateMarketRate model);

        Task<ApiResponse<IEnumerable<MarketRateDto>>> GetAllMarketRates();

        Task<ApiResponse<MarketRateDto>> GetMarketRateById(int id);
        Task<ApiResponse<string>> GetMarketRateBySourceAndDestCurr(string sourceCurr, string destCurr);

    }
}
