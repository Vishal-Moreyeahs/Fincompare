using Fincompare.Application.Repositories;
using Fincompare.Application.Request.MarketRateRequest;
using Fincompare.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Services
{
    public class MarketRateServices : IMarketRateServices
    {
        public Task<ApiResponse<string>> AddMarketRate(AddMarketRate model)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<MarketRateDto>>> GetAllMarketRates()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<MarketRateDto>> GetMarketRateById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> GetMarketRateBySourceAndDestCurr(string sourceCurr, string destCurr)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> UpdateMarketRate(UpdateMarketRate model)
        {
            throw new NotImplementedException();
        }
    }
}
