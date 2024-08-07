using Fincompare.Application.Response;
using static Fincompare.Application.Request.ServiceCategoriesRequest.ServiceCategoriesViewModel;
using static Fincompare.Application.Response.ServiceCategoriesResponse.ServiceCategoriesViewResponse;

namespace Fincompare.Application.Repositories
{
    public interface IServiceCategory
    {
        Task<ApiResponse<CreateServiceCategoriesRequest>> CreateServiceCategories(CreateServiceCategoriesRequest model);
        Task<ApiResponse<IEnumerable<GetAllServiceCategoriesResponse>>> FetchAllServiceCategories(int? idServCategory,/* string? countryIso3,*/ bool? status);
        Task<ApiResponse<CreateServiceCategoriesRequest>> UpdateServiceCategories(UpdateServiceCategoriesRequest model);

    }
}
