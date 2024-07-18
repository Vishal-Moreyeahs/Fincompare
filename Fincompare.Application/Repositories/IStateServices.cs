using Fincompare.Application.Request.StateRequest;
using Fincompare.Application.Response;

namespace Fincompare.Application.Repositories
{
    public interface IStateServices
    {
        Task<ApiResponse<StateDTO>> AddState(AddStateRequest model);
        Task<ApiResponse<StateDTO>> UpdateState(UpdateStateRequest model);
        Task<ApiResponse<IEnumerable<StateDTO>>> GetAllState(string? countryIso3, int? stateId, bool? status);
        Task<ApiResponse<StateDTO>> GetByStateId(int id);
        Task<ApiResponse<IEnumerable<StateDTO>>> GetStateByCountryIso(string country3iso);
        Task<ApiResponse<string>> DeleteState(int id);
    }
}
