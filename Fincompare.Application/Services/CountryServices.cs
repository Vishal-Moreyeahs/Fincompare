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
                        Success = false,
                        Message = "country creation failed"
                    };
                }

                var country = _mapper.Map<Country>(addCountry);
                await _unitOfWork.GetRepository<Country>().Add(country);
                await _unitOfWork.SaveChangesAsync();
                var data = _mapper.Map<CountryRequest>(country);
                var response = new ApiResponse<CountryRequest>()
                {
                    Success = true,
                    Message = "country record created successfully",
                    Data = data
                };
                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("country creation failed");
            }
        }

        public async Task<ApiResponse<List<GetCountryDto>>> GetAllCountry(string? country3iso, bool? status)
        {
            try
            {
                var countries = await _unitOfWork.GetRepository<Country>().GetAll();
                if (countries == null)
                {

                    var res = new ApiResponse<List<GetCountryDto>>()
                    {
                        Success = false,
                        Message = "country fetch failed"
                    };
                    return res;
                }
                if (!string.IsNullOrEmpty(country3iso))
                    countries = countries.Where(x => x.Country3Iso == country3iso);
                if (status.HasValue)
                    countries = countries.Where(x => x.Status == status.Value);


                var data = _mapper.Map<List<GetCountryDto>>(countries);

                if (data == null || data.ToList().Count == 0)
                {
                    return new ApiResponse<List<GetCountryDto>>()
                    {
                        Success = false,
                        Message = "country fetch failed"
                    };
                }

                var response = new ApiResponse<List<GetCountryDto>>()
                {
                    Success = true,
                    Message = "country record fetched successfully",
                    Data = data
                };
                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"country fetch failed {ex.Message}");
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
                        Success = true,
                        Message = "Country Found",
                        Data = data
                    };
                    return response;
                }
                else
                {
                    var response = new ApiResponse<GetCountryDto>()
                    {
                        Success = false,
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
                        Success = true,
                        Message = "country record deleted successfully"
                    };
                    return response;
                }
                else
                {
                    var response = new ApiResponse<string>()
                    {
                        Success = false,
                        Message = "country delete failed"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"country delete failed {ex.Message}");
            }

        }

        public async Task<ApiResponse<CountryRequest>> UpdateCountry(CountryRequest request)
        {
            try
            {
                var countryData = await _unitOfWork.GetRepository<Country>().GetById(request.Country3Iso);
                if (countryData == null)
                    return new ApiResponse<CountryRequest>()
                    {
                        Success = false,
                        Message = "country update failed"
                    };
                var country = _mapper.Map(request, countryData);
                await _unitOfWork.GetRepository<Country>().Upsert(country);
                await _unitOfWork.SaveChangesAsync();

                var data = _mapper.Map<CountryRequest>(country);
                var response = new ApiResponse<CountryRequest>()
                {
                    Success = true,
                    Message = "country record updated successfully",
                    Data = data
                };
                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("country update failed " + ex.Message);
            }
        }
    }
}
