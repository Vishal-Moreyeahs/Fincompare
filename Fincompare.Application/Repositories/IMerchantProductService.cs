using Fincompare.Application.Request.MerchantProductRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.MerchantProductResponse;

namespace Fincompare.Application.Repositories
{
    public interface IMerchantProductService
    {

        Task<ApiResponse<MerchantProductViewModel>> AddMerchantProduct(AddMerchantProductRequest model);
        Task<ApiResponse<IEnumerable<MerchantProductViewModel>>> GetMerchantProductByMerchantId(int merchantId);

        Task<ApiResponse<IEnumerable<MerchantProductViewModel>>> GetMerchantProducts(string sendCountry, string receiveCountry, string sendCurrency, string receiveCurrency, int? merchantID, int? merchantProductID, int? productID, int? serviceCategoryID, int? instrumentID, bool? status);
        Task<ApiResponse<MerchantProductViewModel>> UpdateMerchantProduct(UpdateMerchantProductRequest model);
    }
}
