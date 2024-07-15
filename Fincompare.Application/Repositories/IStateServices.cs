using Fincompare.Application.Request.StateRequest;
using Fincompare.Application.Response;

namespace Fincompare.Application.Repositories
{
    public interface IStateServices
    {
        Task<ApiResponse<string>> AddState(AddStateRequest model);
        Task<ApiResponse<string>> UpdateState(UpdateStateRequest model);
        Task<ApiResponse<IEnumerable<StateDTO>>> GetAllState();
        Task<ApiResponse<StateDTO>> GetByStateId(int id);
        Task<ApiResponse<StateDTO>> GetStateByCountryIso(string country3iso);
        Task<ApiResponse<string>> DeleteState(int id);
    }
}
