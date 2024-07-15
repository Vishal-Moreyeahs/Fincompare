using Fincompare.Application.Request.CityRequest;
using Fincompare.Application.Request.StateRequest;
using Fincompare.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Repositories
{
    public interface ICityServices
    {
        Task<ApiResponse<string>> AddCity(AddCityRequest model);
        Task<ApiResponse<string>> UpdateCity(UpdateCityRequest model);
        Task<ApiResponse<IEnumerable<CityDto>>> GetAllCity();
        Task<ApiResponse<CityDto>> GetByCityId(int id);
        Task<ApiResponse<CityDto>> GetCityByStateId(int stateId);
        Task<ApiResponse<string>> DeleteCity(int cityId);
    }
}
