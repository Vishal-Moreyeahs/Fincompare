using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.CityRequest;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;

namespace Fincompare.Application.Services
{
    public class CityServices : ICityServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CityServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<UpdateCityRequest>> AddCity(AddCityRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<UpdateCityRequest>()
                    {
                        Success = false,
                        Message = "city creation failed",
                    };

                var city = _mapper.Map<City>(model);
                await _unitOfWork.GetRepository<City>().Add(city);
                await _unitOfWork.SaveChangesAsync();

                var response = _mapper.Map<UpdateCityRequest>(city);
                return new ApiResponse<UpdateCityRequest>()
                {
                    Success = true,
                    Message = "city record created successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<string>> DeleteCity(int cityId)
        {
            try
            {
                var city = await _unitOfWork.GetRepository<City>().GetById(cityId);
                if (city != null)
                {
                    city.IsDeleted = true;
                    //city.Status = false;
                    //city.UpdatedDate = DateTime.UtcNow;
                    await _unitOfWork.GetRepository<City>().Upsert(city);
                    await _unitOfWork.SaveChangesAsync();
                    var response = new ApiResponse<string>()
                    {
                        Success = true,
                        Message = "city record deleted successfully"
                    };
                    return response;
                }
                else
                {
                    var response = new ApiResponse<string>()
                    {
                        Success = false,
                        Message = "city delete failed"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{ex.Message}");
            }
        }

        public async Task<ApiResponse<IEnumerable<CityDto>>> GetAllCity(string? countryIso3, int? stateId, int? cityId, bool? status)
        {
            try
            {
                var getAllCity = _unitOfWork.GetRepository<City>().GetAllRelatedEntity();

                if (!string.IsNullOrEmpty(countryIso3))
                {
                    getAllCity = getAllCity.Where(x => x.State.Country3Iso == countryIso3).ToList();
                }
                if (stateId.HasValue)
                {
                    getAllCity = getAllCity.Where(x => x.StateId == stateId).ToList();
                }
                if (cityId.HasValue)
                {
                    getAllCity = getAllCity.Where(x => x.Id == cityId).ToList();
                }
                if (status.HasValue)
                {
                    getAllCity = getAllCity.Where(x => x.Status == status).ToList();
                }

                if (getAllCity.ToList().Count == 0)
                {
                    var response = new ApiResponse<IEnumerable<CityDto>>()
                    {
                        Success = false,
                        Message = "city fetch failed"
                    };
                    return response;
                }
                var data = _mapper.Map<IEnumerable<CityDto>>(getAllCity);
                return new ApiResponse<IEnumerable<CityDto>>()
                {
                    Success = true,
                    Message = "city record fetched successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ApiResponse<CityDto>> GetByCityId(int id)
        {
            try
            {
                var getCity = await _unitOfWork.GetRepository<City>().GetById(id);

                if (getCity == null)
                {
                    var response = new ApiResponse<CityDto>()
                    {
                        Success = false,
                        Message = "city fetch failed"
                    };
                    return response;
                }

                var data = _mapper.Map<CityDto>(getCity);

                return new ApiResponse<CityDto>()
                {
                    Success = true,
                    Message = "city fetched successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<CityDto>> GetCityByStateId(int stateId)
        {
            try
            {
                var checkCity = _unitOfWork.GetRepository<City>().GetAllRelatedEntity();

                if (checkCity == null)
                    return new ApiResponse<CityDto>()
                    {
                        Success = false,
                        Message = "City Not Found !"
                    };

                var city = checkCity.Where(x => x.State.Id == stateId).FirstOrDefault();
                if (city == null)
                    return new ApiResponse<CityDto>()
                    {
                        Success = false,
                        Message = "City Not Found !"
                    };
                var data = _mapper.Map<CityDto>(city);

                return new ApiResponse<CityDto>()
                {
                    Success = true,
                    Message = "City Found !",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<UpdateCityRequest>> UpdateCity(UpdateCityRequest model)
        {
            try
            {
                var checkCity = await _unitOfWork.GetRepository<City>().GetById(model.Id);
                if (checkCity == null)
                    return new ApiResponse<UpdateCityRequest>()
                    {
                        Success = false,
                        Message = "city update failed"
                    };

                var updateCity = _mapper.Map(model, checkCity);
                await _unitOfWork.GetRepository<City>().Upsert(updateCity);
                await _unitOfWork.SaveChangesAsync();
                var response = _mapper.Map<UpdateCityRequest>(updateCity);
                return new ApiResponse<UpdateCityRequest>()
                {
                    Success = true,
                    Message = "city record updated successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
