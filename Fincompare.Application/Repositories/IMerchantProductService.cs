using Fincompare.Application.Request.MerchantProductRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.MerchantProductResponse;
using Fincompare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Repositories
{
    public interface IMerchantProductService
    {

        Task<ApiResponse<MerchantProductViewModel>> AddMerchantProduct(AddMerchantProductRequest model);
        Task<ApiResponse<IEnumerable<MerchantProductViewModel>>> GetMerchantProductByMerchantId(int merchantId);

        ApiResponse<IEnumerable<MerchantProductViewModel>> GetMerchantProducts(string sendCountry, string receiveCountry, string sendCurrency, string receiveCurrency, int? merchantID, int? merchantProductID, int? productID, int? serviceCategoryID, int? instrumentID, bool? status);
        Task<ApiResponse<MerchantProductViewModel>> UpdateMerchantProduct(UpdateMerchantProductRequest model);
    }
}
