using Fincompare.Application.Request.CityRequest;
using Fincompare.Application.Response;

namespace Fincompare.Application.Repositories
{
    public interface ICityServices
    {
        Task<ApiResponse<UpdateCityRequest>> AddCity(AddCityRequest model);
        Task<ApiResponse<UpdateCityRequest>> UpdateCity(UpdateCityRequest model);
        Task<ApiResponse<IEnumerable<CityDto>>> GetAllCity(string? countryIso3, int? StateId, int? CityId, bool? Status);
        Task<ApiResponse<CityDto>> GetByCityId(int id);
        Task<ApiResponse<CityDto>> GetCityByStateId(int stateId);
        Task<ApiResponse<string>> DeleteCity(int cityId);
    }
}
