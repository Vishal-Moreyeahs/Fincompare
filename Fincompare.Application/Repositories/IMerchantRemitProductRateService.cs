using Fincompare.Application.Request.MerchantRemitProductRateRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.MerchantRemitProductRateResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Repositories
{
    public interface IMerchantRemitProductRateService
    {
        Task<ApiResponse<MerchantRemitProductRateViewModel>> AddMerchantRemitProductRate(AddMerchantRemitProductRateRequest model);

        Task<ApiResponse<IEnumerable<MerchantRemitProductRateViewModel>>> GetAllMerchantRemitProductRate(string sendCountry,
            string receiveCountry,
            string sendCurrency,
            string receiveCurrency,
            int? merchantId,
            int? remittanceRateId,
            int? merchantProductId,
            int? serviceCategoryId,
            int? instrumentId,
            double? sendMinLimit,
            double? receiveMinLimit,
            bool? status);
        
        Task<ApiResponse<IEnumerable<MerchantRemitProductRateViewModel>>> GetAllMerchantRemitProductRateByCurrencyPairAndMerchant(string sendCurrency,
            string receiveCurrency,
            int merchantId,
            int? remittanceRateId,
            int? merchantProductId,
            int? serviceCategoryId,
            int? instrumentId,
            double? sendMinLimit,
            double? receiveMinLimit,
            bool? status);

        Task<ApiResponse<IEnumerable<MerchantRemitProductRateViewModel>>> GetAllMerchantRemitProductRateByMerchant(int merchantId);

    }
}
