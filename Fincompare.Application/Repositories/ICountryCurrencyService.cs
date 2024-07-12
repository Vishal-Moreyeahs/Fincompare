using Fincompare.Application.Response;
using static Fincompare.Application.Request.CountryCurrencyRequest.CountryCurrencyBaseModel;
using static Fincompare.Application.Response.CountryCurrencyResponse.CountryCurrencyResponseBaseClass;
using static Fincompare.Application.Response.CurrencyResponse.CurrencyResponseBaseModel;

namespace Fincompare.Application.Repositories
{
    public interface ICountryCurrencyService
    {
        Task<ApiResponse<string>> AddCountryCurrency(AddCountryCurrencyRequest model);
        Task<ApiResponse<string>> UpdateCountryCurrency(UpdateCountryCurrency model);
        Task<ApiResponse<IEnumerable<GetAllCountryCurrencyResponse>>> GetAllCountryCurrency();
        Task<ApiResponse<GetAllCountryCurrencyResponse>> GetCountryCurrencyById(int id);
        Task<ApiResponse<string>> DeleteCountryCurrency(int id);
    }
}
