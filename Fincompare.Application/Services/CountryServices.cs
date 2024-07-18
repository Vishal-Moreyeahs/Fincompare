using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.CountryRequest;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;

namespace Fincompare.Application.Services
{
    public class CountryServices : ICountryServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CountryRequest>> AddCountry(CountryRequest addCountry)
        {
            try
            {
                var checkCountry = await _unitOfWork.GetRepository<Country>().GetById(addCountry.Country3Iso);

                if (checkCountry != null)
                {
                    return new ApiResponse<CountryRequest>()
                    {
                        Status = true,
                        Message = "Country already exist"
                    };
                }

                var country = _mapper.Map<Country>(addCountry);
                //country.CreatedDate = DateTime.UtcNow;
                //country.UpdatedDate = DateTime.UtcNow;
                await _unitOfWork.GetRepository<Country>().Add(country);
                await _unitOfWork.SaveChangesAsync();
                var data = _mapper.Map<CountryRequest>(country);
                var response = new ApiResponse<CountryRequest>()
                {
                    Status = true,
                    Message = "Country Created Successfully",
                    Data = data
                };
                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Country not created");
            }

        }

        public async Task<ApiResponse<List<GetCountryDto>>> GetAllCountry()
        {
            try
            {
                var countries = await _unitOfWork.GetRepository<Country>().GetAll();
                if (countries == null)
                {

                    var res = new ApiResponse<List<GetCountryDto>>()
                    {
                        Status = false,
                        Message = "Countries not found"
                    };
                    return res;
                }
                var data = _mapper.Map<List<GetCountryDto>>(countries);
                var response = new ApiResponse<List<GetCountryDto>>()
                {
                    Status = true,
                    Message = "Get countries",
                    Data = data
                };
                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{ex.Message}");
            }
        }

        public async Task<ApiResponse<GetCountryDto>> GetCountryByCountryName(string country3Iso)
        {
            try
            {
                var countryList = await _unitOfWork.GetRepository<Country>().GetAll();
                var country = countryList.Where(x => x.Country3Iso == country3Iso).FirstOrDefault();
                if (country != null)
                {
                    var data = _mapper.Map<GetCountryDto>(country);
                    var response = new ApiResponse<GetCountryDto>()
                    {
                        Status = true,
                        Message = "Country Found",
                        Data = data
                    };
                    return response;
                }
                else
                {
                    var response = new ApiResponse<GetCountryDto>()
                    {
                        Status = false,
                        Message = "Country not found"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{ex.Message}");
            }
        }

        public async Task<ApiResponse<string>> RemoveCountry(string country3Iso)
        {
            try
            {
                var countryList = await _unitOfWork.GetRepository<Country>().GetAll();
                var country = countryList.Where(x => x.Country3Iso == country3Iso).FirstOrDefault();
                if (country != null)
                {
                    //country.Status = false;
                    country.IsDeleted = true;
                    //country.UpdatedDate = DateTime.UtcNow;
                    await _unitOfWork.GetRepository<Country>().Upsert(country);
                    await _unitOfWork.SaveChangesAsync();
                    var response = new ApiResponse<string>()
                    {
                        Status = true,
                        Message = "Country Deleted Successfully"
                    };
                    return response;
                }
                else
                {
                    var response = new ApiResponse<string>()
                    {
                        Status = false,
                        Message = "Country not found"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{ex.Message}");
            }

        }

        public async Task<ApiResponse<string>> UpdateCountry(CountryRequest request)
        {
            try
            {
                var countryData = await _unitOfWork.GetRepository<Country>().GetById(request.Country3Iso);
                if (countryData == null)
                    return new ApiResponse<string>()
                    {
                        Status = false,
                        Message = "Country Updated Successfully"
                    };
                var country = _mapper.Map(request, countryData);
                await _unitOfWork.GetRepository<Country>().Upsert(countryData);
                await _unitOfWork.SaveChangesAsync();
                var response = new ApiResponse<string>()
                {
                    Status = true,
                    Message = "Country Updated Successfully"
                };
                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Country not created" + ex.Message);
            }
        }
    }
}
