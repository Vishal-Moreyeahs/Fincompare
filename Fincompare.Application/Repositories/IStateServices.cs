using Fincompare.Application.Request.StateRequest;
using Fincompare.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fincompare.Application.Request.CurrencyRequest.CurrencyRequests;
using static Fincompare.Application.Response.CurrencyResponse.CurrencyResponseBaseModel;

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
