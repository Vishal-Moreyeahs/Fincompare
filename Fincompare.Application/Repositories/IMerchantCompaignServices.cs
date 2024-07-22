using Fincompare.Application.Request.CityRequest;
using Fincompare.Application.Request.MerchantCompaignRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.MerchantCompaignResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Repositories
{
    public interface IMerchantCompaignServices
    {
        Task<ApiResponse<MerchantCompaignResponseViewModel>> AddMerchantCompaign(AddMerchantCompaignRequest model);
        Task<ApiResponse<MerchantCompaignResponseViewModel>> UpdateMerchantCompaign(UpdateMerchantCompaignRequest model);
        Task<ApiResponse<IEnumerable<MerchantCompaignResponseViewModel>>> GetMerchantCampaigns(
            int? MerchantCampaignId,
            int? MerchantID,
            string sendCountry,
            string receiveCountry,
            //string sendCurrency,
            //string receiveCurrency,
            int? MerchantProductID,
            int? serviceCategoryId,
            int? instrumentId,
            DateTime? dateValidity
            //decimal? SendMinLimit,
            /*decimal? ReceiveMinLimit*/);
    };
    
}
