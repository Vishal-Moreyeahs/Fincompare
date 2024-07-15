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

        public async Task<ApiResponse<string>> AddState(AddStateRequest model)
        {
            try
            {
                var state = _mapper.Map<State>(model);
                //state.CreatedDate = DateTime.UtcNow;
                //state.UpdatedDate = DateTime.UtcNow;
                await _unitOfWork.GetRepository<State>().Add(state);
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse<string>()
                {
                    Status = true,
                    Message = "State Added Successfully !",
                };
            }
            catch (Exception ex)
            {
                throw ex;
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
                        Status = true,
                        Message = "State Deleted Successfully"
                    };
                    return response;
                }
                else
                {
                    var response = new ApiResponse<string>()
                    {
                        Status = false,
                        Message = "State not found"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{ex.Message}");
            }

        }

        public async Task<ApiResponse<IEnumerable<StateDTO>>> GetAllState()
        {
            try
            {
                var getAllState = await _unitOfWork.GetRepository<State>().GetAll();

                if (getAllState == null || getAllState.ToList().Count == 0)
                {
                    var response = new ApiResponse<IEnumerable<StateDTO>>()
                    {
                        Status = false,
                        Message = "State Not Found !"
                    };
                    return response;
                }
                var data = _mapper.Map<IEnumerable<StateDTO>>(getAllState);
                return new ApiResponse<IEnumerable<StateDTO>>()
                {
                    Status = true,
                    Message = "State Found !",
                    Data = data
                };
            }
            catch (Exception ex)
            {

                throw ex;
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
                        Status = false,
                        Message = "State Not Found !"
                    };
                    return response;
                }

                var data = _mapper.Map<StateDTO>(getState);

                return new ApiResponse<StateDTO>()
                {
                    Status = true,
                    Message = "State Found !",
                    Data = data
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ApiResponse<StateDTO>> GetStateByCountryIso(string country3iso)
        {
            try
            {
                var checkState = _unitOfWork.GetRepository<State>().GetAllRelatedEntity();

                if (checkState == null)
                    return new ApiResponse<StateDTO>()
                    {
                        Status = false,
                        Message = "State Not Found !"
                    };

                var state = checkState.Where(x => x.Country3IsoNavigation.Country3Iso == country3iso).FirstOrDefault();
                if (state == null)
                    return new ApiResponse<StateDTO>()
                    {
                        Status = false,
                        Message = "State Not Found !"
                    };
                var data = _mapper.Map<StateDTO>(state);

                return new ApiResponse<StateDTO>()
                {
                    Status = true,
                    Message = "State Found !",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<string>> UpdateState(UpdateStateRequest model)
        {
            try
            {
                var checkState = await _unitOfWork.GetRepository<State>().GetById(model.Id);
                if (checkState == null)
                    return new ApiResponse<string>()
                    {
                        Status = false,
                        Message = "State Not Found !"
                    };

                var updateState = _mapper.Map(model, checkState);
                //updateState.UpdatedDate = DateTime.UtcNow;
                await _unitOfWork.GetRepository<State>().Upsert(updateState);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<string>()
                {
                    Status = true,
                    Message = "State Update Successfully !",
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
