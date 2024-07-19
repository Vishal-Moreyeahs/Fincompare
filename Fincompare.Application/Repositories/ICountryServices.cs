using Fincompare.Application.Request.CountryRequest;
using Fincompare.Application.Response;

namespace Fincompare.Application.Repositories
{
    public interface ICountryServices
    {
        Task<ApiResponse<CountryRequest>> AddCountry(CountryRequest addCountry);
        Task<ApiResponse<string>> RemoveCountry(string country3Iso);
        Task<ApiResponse<CountryRequest>> UpdateCountry(CountryRequest request);
        Task<ApiResponse<GetCountryDto>> GetCountryByCountryName(string countryName);
        Task<ApiResponse<List<GetCountryDto>>> GetAllCountry(string? country3iso, bool? status);
    }
}
