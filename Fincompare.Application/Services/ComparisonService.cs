using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Services
{
    public class ConversionService : IConversionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMarketRateServices _marketRateServices;

        public ConversionService(IUnitOfWork unitOfWork, IMarketRateServices marketRateServices)
        { 
            _unitOfWork = unitOfWork;
            _marketRateServices = marketRateServices;
        }
        public async Task<ApiResponse<ConversionResponseViewModel>> GetConversionResult(string sendCurrency, string receiveCurrency, double sendAmount)
        {
            var response = new ApiResponse<ConversionResponseViewModel>();
            var result = await _marketRateServices.GetMarketRateBySourceAndDestCurr(sendCurrency,receiveCurrency);

            if (!result.Success)
            {
                response.Message = "conversion record fetch failed";
                return response;
            }

            var data = new ConversionResponseViewModel
                        {
                            SendCurrency = sendCurrency,
                            ReceiveCurrency = receiveCurrency,
                            Amount = sendAmount * result.Data.Rate
                        };

            response.Success = true;
            response.Message = "conversion fetch successfully";
            response.Data = data;
            return response;
        }

    }

    public class ConversionResponseViewModel
    { 
        public double Amount { get; set; }
        public string SendCurrency { get; set; }
        public string ReceiveCurrency { get; set; }
    }

}
