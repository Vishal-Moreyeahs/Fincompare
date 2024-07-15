using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.CityRequest;
using Fincompare.Application.Request.StateRequest;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<ApiResponse<string>> AddCity(AddCityRequest model)
        {
            try
            {
                var city = _mapper.Map<City>(model);
                city.CreatedDate = DateTime.UtcNow;
                city.UpdatedDate = DateTime.UtcNow;
                await _unitOfWork.GetRepository<City>().Add(city);
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse<string>()
                {
                    Status = true,
                    Message = "City Added Successfully !",
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
                    city.Status = false;
                    city.UpdatedDate = DateTime.UtcNow;
                    await _unitOfWork.GetRepository<City>().Upsert(city);
                    await _unitOfWork.SaveChangesAsync();
                    var response = new ApiResponse<string>()
                    {
                        Status = true,
                        Message = "City Deleted Successfully"
                    };
                    return response;
                }
                else
                {
                    var response = new ApiResponse<string>()
                    {
                        Status = false,
                        Message = "City not found"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{ex.Message}");
            }
        }

        public async Task<ApiResponse<IEnumerable<CityDto>>> GetAllCity()
        {
            try
            {
                var getAllCity = await _unitOfWork.GetRepository<City>().GetAll();

                if (getAllCity == null || getAllCity.ToList().Count == 0)
                {
                    var response = new ApiResponse<IEnumerable<CityDto>>()
                    {
                        Status = false,
                        Message = "City Not Found !"
                    };
                    return response;
                }
                var data = _mapper.Map<IEnumerable<CityDto>>(getAllCity);
                return new ApiResponse<IEnumerable<CityDto>>()
                {
                    Status = true,
                    Message = "City Found !",
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
                        Status = false,
                        Message = "State Not Found !"
                    };
                    return response;
                }

                var data = _mapper.Map<CityDto>(getCity);

                return new ApiResponse<CityDto>()
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

        public async Task<ApiResponse<CityDto>> GetCityByStateId(int stateId)
        {
            try
            {
                var checkCity = _unitOfWork.GetRepository<City>().GetAllRelatedEntity();

                if (checkCity == null)
                    return new ApiResponse<CityDto>()
                    {
                        Status = false,
                        Message = "City Not Found !"
                    };

                var city = checkCity.Where(x => x.State.Id == stateId).FirstOrDefault();
                if (city == null)
                    return new ApiResponse<CityDto>()
                    {
                        Status = false,
                        Message = "City Not Found !"
                    };
                var data = _mapper.Map<CityDto>(city);

                return new ApiResponse<CityDto>()
                {
                    Status = true,
                    Message = "City Found !",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<string>> UpdateCity(UpdateCityRequest model)
        {
            try
            {
                var checkCity = await _unitOfWork.GetRepository<City>().GetById(model.Id);
                if (checkCity == null)
                    return new ApiResponse<string>()
                    {
                        Status = false,
                        Message = "City Not Found !"
                    };

                var updateCity = _mapper.Map(model, checkCity);
                updateCity.UpdatedDate = DateTime.UtcNow;
                await _unitOfWork.GetRepository<City>().Upsert(updateCity);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<string>()
                {
                    Status = true,
                    Message = "City Update Successfully !",
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
