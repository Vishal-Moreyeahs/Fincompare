using Fincompare.Application.Response;
using static Fincompare.Application.Request.ProductRequests.ProductRequestViewModel;
using static Fincompare.Application.Response.ProductResponse.ProductResponseBaseClass;

namespace Fincompare.Application.Repositories
{
    public interface IProductService
    {
        Task<ApiResponse<string>> CreateProduct(CreateProductRequest model);
        Task<ApiResponse<string>> UpdateProduct(UpdateProductRequest model);
        Task<ApiResponse<IEnumerable<GetAllProductResponse>>> GetAllProduct();
        Task<ApiResponse<GetAllProductResponse>> GetProductById(int id);
    }
}
