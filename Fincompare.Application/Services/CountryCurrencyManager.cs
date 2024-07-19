using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.CountryCurrencyRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.CountryCurrencyResponse;
using Fincompare.Domain.Entities;
using static Fincompare.Application.Response.CurrencyResponse.CurrencyResponseBaseModel;

namespace Fincompare.Application.Services
{
    public class CountryCurrencyManager : ICountryCurrencyManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryCurrencyManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<GetCountryCurrencyResponse>>> GetCurrenciesbyCountry3Iso(string? country3Iso, string? categoryId)
        {
            try
            {
                var currencyList = _unitOfWork.GetRepository<CountryCurrency>().GetAllRelatedEntity();

                var currencies = new List<GetCountryCurrencyResponse>();

                currencies = currencyList.Select(cc => new GetCountryCurrencyResponse
                                    {
                                        Id = cc.Id,
                                        CurrencyIso = cc.CurrencyIso,
                                        Country3Iso = cc.Country3Iso,
                                        IsPrimary = cc.IsPrimaryCur,
                                        Category = cc.CountryCurrencyCategoryId,
                                        Status = cc.Status
                                    }).ToList();

                if (!string.IsNullOrEmpty(categoryId))
                {
                    currencies = currencyList
                                            .Where(cc => cc.CountryCurrencyCategoryId == categoryId)
                                            .Select(cc => new GetCountryCurrencyResponse
                                            {
                                                Id = cc.Id,
                                                CurrencyIso = cc.CurrencyIso,
                                                Country3Iso = cc.Country3Iso,
                                                IsPrimary = cc.IsPrimaryCur,
                                                Category = cc.CountryCurrencyCategoryId,
                                                Status = cc.Status
                                            }).ToList();
                }
                if(!string.IsNullOrEmpty(country3Iso))
                {
                    currencies = currencyList
                                                .Where(cc => cc.Country3Iso == country3Iso)
                                                .Select(cc => new GetCountryCurrencyResponse
                                                {
                                                    Id = cc.Id,
                                                    CurrencyIso = cc.CurrencyIso,
                                                    Country3Iso = cc.Country3Iso,
                                                    IsPrimary = cc.IsPrimaryCur,
                                                    Category = cc.CountryCurrencyCategoryId,
                                                    Status = cc.Status
                                                }).ToList();

                }

                if (!string.IsNullOrEmpty(country3Iso) && !string.IsNullOrEmpty(categoryId))
                {
                    currencies = currencyList
                                                .Where(cc => cc.Country3Iso == country3Iso && cc.CountryCurrencyCategoryId == categoryId)
                                                .Select(cc => new GetCountryCurrencyResponse
                                                {
                                                    Id = cc.Id,
                                                    CurrencyIso = cc.CurrencyIso,
                                                    Country3Iso = cc.Country3Iso,
                                                    IsPrimary = cc.IsPrimaryCur,
                                                    Category = cc.CountryCurrencyCategoryId,
                                                    Status = cc.Status
                                                }).ToList();

                }

                var response = new ApiResponse<List<GetCountryCurrencyResponse>>()
                {
                    Status = true,
                    Message = "Currencies fetched",
                    Data = currencies
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<List<GetCountryCurrencyResponse>>> AddCountryWithMultipleCurrencies(UpdateCountryWithMultipleCurrencyRequest model)
        {
            try
            {
                // Find the country by ISO code
                var countries = _unitOfWork.GetRepository<Country>().GetAllRelatedEntity();

                var country = countries.FirstOrDefault(c => c.Country3Iso == model.Country3Iso);

                if (country == null)
                {
                    throw new ApplicationException("Country not found");
                }

                // Add new CountryCurrencies
                var newCountryCurrencies = model.Currencies.Select(c => new CountryCurrency
                {
                    Country3Iso = model.Country3Iso,
                    //CurrencyId = c.Id,
                    CurrencyIso = c.CurrencyIso,
                    IsPrimaryCur = c.IsPrimary,
                    CountryCurrencyCategoryId = c.Category,
                    //UpdatedDate = DateTime.UtcNow,
                    //CreatedDate = DateTime.UtcNow
                }).ToList();

                await _unitOfWork.GetRepository<CountryCurrency>().AddRange(newCountryCurrencies);
                await _unitOfWork.SaveChangesAsync();

                var data = newCountryCurrencies.Select(cc => new GetCountryCurrencyResponse
                                    {
                                        Id = cc.Id,
                                        CurrencyIso = cc.CurrencyIso,
                                        Country3Iso = cc.Country3Iso,
                                        IsPrimary = cc.IsPrimaryCur,
                                        Category = cc.CountryCurrencyCategoryId,
                                        Status = cc.Status
                                    }).ToList();

                var response = new ApiResponse<List<GetCountryCurrencyResponse>>()
                {
                    Status = true,
                    Message = $"Country Currencies Created successfully",
                    Data = data
                };
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<GetCountryCurrencyResponse>> UpdateCountryCurrency(UpdateCountryCurrencyRequest model)
        {
            try
            {
                var response = new ApiResponse<GetCountryCurrencyResponse>();
                var checkCountryCurrency = await _unitOfWork.GetRepository<CountryCurrency>().GetById(model.Id);

                if (checkCountryCurrency == null)
                {
                    response.Message = "Country Currency not found";
                    return response;
                }

                // Map the updated data and save it in the database
                _mapper.Map(model, checkCountryCurrency);
               
                await _unitOfWork.GetRepository<CountryCurrency>().Upsert(checkCountryCurrency);
                await _unitOfWork.SaveChangesAsync();

                var data = new GetCountryCurrencyResponse
                {
                    Id = checkCountryCurrency.Id,
                    CurrencyIso = checkCountryCurrency.CurrencyIso,
                    Country3Iso = checkCountryCurrency.Country3Iso,
                    IsPrimary = checkCountryCurrency.IsPrimaryCur,
                    Category = checkCountryCurrency.CountryCurrencyCategoryId,
                    Status = checkCountryCurrency.Status
                };

                response.Message = "country currency updated successfully";
                response.Status = true;
                response.Data = data;
                return response;
            }

            catch (Exception ex) { 
                throw new ApplicationException($"{ex.Message}");
            }
            

        }
    }
}
