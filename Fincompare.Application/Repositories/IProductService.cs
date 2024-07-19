using Fincompare.Application.Response;
using static Fincompare.Application.Request.ProductRequests.ProductRequestViewModel;
using static Fincompare.Application.Response.ProductResponse.ProductResponseBaseClass;

namespace Fincompare.Application.Repositories
{
    public interface IProductService
    {
        Task<ApiResponse<GetAllProductResponse>> CreateProduct(CreateProductRequest model);
        Task<ApiResponse<CreateProductRequest>> UpdateProduct(UpdateProductRequest model);
        Task<ApiResponse<IEnumerable<GetAllProductResponse>>> GetAllProduct(string? countryIso3, int? idProduct, int? idServCategory, bool? status);
        Task<ApiResponse<GetAllProductResponse>> GetProductById(int id);
    }
}
