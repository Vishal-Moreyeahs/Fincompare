using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.StateRequest;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;

namespace Fincompare.Application.Services
{
    public class StateServices : IStateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StateServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<StateDTO>> AddState(AddStateRequest model)
        {
            try
            {
                var state = _mapper.Map<State>(model);
                //state.CreatedDate = DateTime.UtcNow;
                //state.UpdatedDate = DateTime.UtcNow;
                await _unitOfWork.GetRepository<State>().Add(state);
                await _unitOfWork.SaveChangesAsync();

                var data = _mapper.Map<StateDTO>(state);

                return new ApiResponse<StateDTO>()
                {
                    Success = true,
                    Message = "state record created successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"state creation failed {ex.Message}");
            }
        }

        public async Task<ApiResponse<string>> DeleteState(int id)
        {
            try
            {
                var state = await _unitOfWork.GetRepository<State>().GetById(id);
                if (state != null)
                {
                    //state.Status = false;
                    //state.UpdatedDate = DateTime.UtcNow;
                    state.IsDeleted = true;
                    await _unitOfWork.GetRepository<State>().Upsert(state);
                    await _unitOfWork.SaveChangesAsync();
                    var response = new ApiResponse<string>()
                    {
                        Success = true,
                        Message = "state record deleted successfully"
                    };
                    return response;
                }
                else
                {
                    var response = new ApiResponse<string>()
                    {
                        Success = false,
                        Message = "state delete failed"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($" state delete failed {ex.Message}");
            }

        }

        public async Task<ApiResponse<IEnumerable<StateDTO>>> GetAllState(string? countryIso3, int? stateId, bool? status)
        {
            try
            {
                var getAllState = await _unitOfWork.GetRepository<State>().GetAll();

                if (getAllState == null || getAllState.ToList().Count == 0)
                {
                    var response = new ApiResponse<IEnumerable<StateDTO>>()
                    {
                        Success = false,
                        Message = "state fetch failed"
                    };
                    return response;
                }

                if (!string.IsNullOrEmpty(countryIso3))
                    getAllState = getAllState.Where(x => x.Country3Iso == countryIso3);
                if (stateId.HasValue)
                    getAllState = getAllState.Where(x => x.Id == stateId.Value);
                if (status.HasValue)
                    getAllState = getAllState.Where(x => x.Status == status);


                var data = _mapper.Map<IEnumerable<StateDTO>>(getAllState);

                if (data == null || data.ToList().Count == 0)
                {
                    return new ApiResponse<IEnumerable<StateDTO>>()
                    {
                        Success = false,
                        Message = "state fetch failed"
                    };
                }
                return new ApiResponse<IEnumerable<StateDTO>>()
                {
                    Success = true,
                    Message = "state record fetched successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {

                throw new ApplicationException($"state fetch failed {ex.Message}");
            }
        }

        public async Task<ApiResponse<StateDTO>> GetByStateId(int id)
        {
            try
            {
                var getState = await _unitOfWork.GetRepository<State>().GetById(id);

                if (getState == null)
                {
                    var response = new ApiResponse<StateDTO>()
                    {
                        Success = false,
                        Message = "State Not Found !"
                    };
                    return response;
                }

                var data = _mapper.Map<StateDTO>(getState);

                return new ApiResponse<StateDTO>()
                {
                    Success = true,
                    Message = "State Found !",
                    Data = data
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ApiResponse<IEnumerable<StateDTO>>> GetStateByCountryIso(string country3iso)
        {
            try
            {
                var checkState = _unitOfWork.GetRepository<State>().GetAllRelatedEntity();

                if (checkState == null)
                    return new ApiResponse<IEnumerable<StateDTO>>()
                    {
                        Success = false,
                        Message = "State Not Found !"
                    };

                var state = checkState.Where(x => x.Country3IsoNavigation.Country3Iso == country3iso);
                if (state == null)
                    return new ApiResponse<IEnumerable<StateDTO>>()
                    {
                        Success = false,
                        Message = "State Not Found !"
                    };
                var data = _mapper.Map<IEnumerable<StateDTO>>(state);

                return new ApiResponse<IEnumerable<StateDTO>>()
                {
                    Success = true,
                    Message = "State Found !",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<StateDTO>> UpdateState(UpdateStateRequest model)
        {
            try
            {
                var checkState = await _unitOfWork.GetRepository<State>().GetById(model.Id);
                if (checkState == null)
                    return new ApiResponse<StateDTO>()
                    {
                        Success = false,
                        Message = "state update failed"
                    };

                var updateState = _mapper.Map(model, checkState);
                //updateState.UpdatedDate = DateTime.UtcNow;
                await _unitOfWork.GetRepository<State>().Upsert(updateState);
                await _unitOfWork.SaveChangesAsync();

                var data = _mapper.Map<StateDTO>(updateState);

                return new ApiResponse<StateDTO>()
                {
                    Success = true,
                    Message = "state record update successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"state update failed {ex.Message}");
            }
        }
    }
}
