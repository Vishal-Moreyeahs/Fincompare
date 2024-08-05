using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.CountryCurrencyRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.CountryCurrencyResponse;
using Fincompare.Domain.Entities;

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
                if (!string.IsNullOrEmpty(country3Iso))
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

                if (currencies.Count > 0)
                {
                    return new ApiResponse<List<GetCountryCurrencyResponse>>()
                    {
                        Success = true,
                        Message = "country currencies record fetched successfully",
                        Data = currencies
                    };
                }

                return new ApiResponse<List<GetCountryCurrencyResponse>>()
                {
                    Success = false,
                    Message = "country currencies fetch failed",
                    Data = currencies
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"country currencies fetch failed {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<GetCountryCurrencyResponse>>> AddCountryWithMultipleCurrencies(UpdateCountryWithMultipleCurrencyRequest model)
        {
            try
            {
                // Find the country by ISO code
                var countries = _unitOfWork.GetRepository<Country>().GetAllRelatedEntity();

                var country = countries.FirstOrDefault(c => c.Country3Iso.Trim().ToLower() == model.Country3Iso.Trim().ToLower());


                if (country == null)
                {
                    throw new ApplicationException("country currencies creation failed");
                }


                var allowedCategories = new[] { "1_Orig", "1_Dest" };

                // Add new CountryCurrencies
                var newCountryCurrencies = model.Currencies
                    .Select(c =>
                    {
                        // Check if the category is valid (case-insensitive)
                        if (!allowedCategories.Contains(c.Category.Trim(), StringComparer.OrdinalIgnoreCase))
                        {
                            throw new ArgumentException($"Invalid category: {c.Category}. Allowed categories are '1_Orig' and '1_Dest'.");
                        }

                        return new CountryCurrency
                        {
                            Country3Iso = model.Country3Iso.Trim().ToUpper(),
                            CurrencyIso = c.CurrencyIso.Trim().ToUpper(),
                            IsPrimaryCur = c.IsPrimary,
                            CountryCurrencyCategoryId = c.Category
                        };
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
                    Success = true,
                    Message = $"country Currencies record created successfully",
                    Data = data
                };
                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"country currencies creation failed {ex.Message}");
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
                    response.Message = "country currency update failed";
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

                response.Message = "country currency record updated successfully";
                response.Success = true;
                response.Data = data;
                return response;
            }

            catch (Exception ex)
            {
                throw new ApplicationException($"country currency update failed {ex.Message}");
            }


        }
    }
}
