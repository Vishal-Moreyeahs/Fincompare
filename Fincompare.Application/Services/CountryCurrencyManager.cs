using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.CountryCurrencyRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.CountryCurrencyResponse;
using Fincompare.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Fincompare.Application.Services
{
    public class CountryCurrencyManager : ICountryCurrencyManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CountryCurrencyManager(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ApiResponse<List<GetCountryCurrencyResponse>>> GetCurrenciesbyCountry3Iso(string? country3Iso, string? categoryId, string? currencyIso)
        {
            try
            {
                var currencyList = _unitOfWork.GetRepository<CountryCurrency>().GetAllRelatedEntity();

                var defaultCountryIso = _configuration.GetValue<string>("DefaultCountryIso3");

                var currencies = new List<GetCountryCurrencyResponse>();

                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "ImageJson", "CountryLogo.json");

                // Read the file content
                var jsonData = System.IO.File.ReadAllText(filePath);

                // Optionally, deserialize the JSON into a C# object
                var countries = JsonConvert.DeserializeObject<List<CountryLogoJson>>(jsonData);

                currencies = currencyList.Select(cc => new GetCountryCurrencyResponse
                {
                    Id = cc.Id,
                    CurrencyIso = cc.CurrencyIso,
                    CountryCode = cc.Country3IsoNavigation.Country2Iso,
                    CountryName = cc.Country3IsoNavigation.CountryName,
                    CountryFlag = countries.Any(x => x.CountryIso == cc.Country3Iso) ? countries.Where(x => x.CountryIso == cc.Country3Iso).Select(x => x.FlagUrl).FirstOrDefault() : cc.Country3IsoNavigation.WebLink,
                    Country3Iso = cc.Country3Iso,
                    IsPrimary = cc.IsPrimaryCur,
                    Category = cc.CountryCurrencyCategoryId,
                    Status = cc.Status,
                    IsDefault = cc.Country3Iso.ToLower().Trim() == defaultCountryIso.ToLower().Trim() ? true: false 
                }).ToList();

                if (!string.IsNullOrEmpty(categoryId))
                {
                    currencies = currencyList
                                            .Where(cc => cc.CountryCurrencyCategoryId == categoryId)
                                            .Select(cc => new GetCountryCurrencyResponse
                                            {
                                                Id = cc.Id,
                                                CurrencyIso = cc.CurrencyIso,
                                                CountryCode = cc.Country3IsoNavigation.Country2Iso,
                                                CountryName = cc.Country3IsoNavigation.CountryName,
                                                CountryFlag = countries.Any(x => x.CountryIso == cc.Country3Iso) ? countries.Where(x => x.CountryIso == cc.Country3Iso).Select(x => x.FlagUrl).FirstOrDefault() : cc.Country3IsoNavigation.WebLink,
                                                Country3Iso = cc.Country3Iso,
                                                IsPrimary = cc.IsPrimaryCur,
                                                Category = cc.CountryCurrencyCategoryId,
                                                Status = cc.Status,
                                                IsDefault = cc.Country3Iso.ToLower().Trim() == defaultCountryIso ? true : false
                                            }).ToList();
                }
                if (!string.IsNullOrEmpty(currencyIso))
                {
                    currencies = currencyList
                                            .Where(cc => cc.CurrencyIso == currencyIso)
                                            .Select(cc => new GetCountryCurrencyResponse
                                            {
                                                Id = cc.Id,
                                                CurrencyIso = cc.CurrencyIso,
                                                CountryCode = cc.Country3IsoNavigation.Country2Iso,
                                                CountryName = cc.Country3IsoNavigation.CountryName,
                                                CountryFlag = countries.Any(x => x.CountryIso == cc.Country3Iso) ? countries.Where(x => x.CountryIso == cc.Country3Iso).Select(x => x.FlagUrl).FirstOrDefault() : cc.Country3IsoNavigation.WebLink,
                                                Country3Iso = cc.Country3Iso,
                                                IsPrimary = cc.IsPrimaryCur,
                                                Category = cc.CountryCurrencyCategoryId,
                                                Status = cc.Status,
                                                IsDefault = cc.Country3Iso.ToLower().Trim() == defaultCountryIso ? true : false
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
                                                    CountryCode = cc.Country3IsoNavigation.Country2Iso,
                                                    CountryName = cc.Country3IsoNavigation.CountryName,
                                                    CountryFlag = countries.Any(x => x.CountryIso == cc.Country3Iso) ? countries.Where(x => x.CountryIso == cc.Country3Iso).Select(x => x.FlagUrl).FirstOrDefault() : cc.Country3IsoNavigation.WebLink,
                                                    Country3Iso = cc.Country3Iso,
                                                    IsPrimary = cc.IsPrimaryCur,
                                                    Category = cc.CountryCurrencyCategoryId,
                                                    Status = cc.Status,
                                                    IsDefault = cc.Country3Iso.ToLower().Trim() == defaultCountryIso ? true : false

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
                                                    CountryCode = cc.Country3IsoNavigation.Country2Iso,
                                                    CountryName = cc.Country3IsoNavigation.CountryName,
                                                    CountryFlag = countries.Any(x => x.CountryIso == cc.Country3Iso) ? countries.Where(x => x.CountryIso == cc.Country3Iso).Select(x => x.FlagUrl).FirstOrDefault() : cc.Country3IsoNavigation.WebLink,
                                                    Country3Iso = cc.Country3Iso,
                                                    IsPrimary = cc.IsPrimaryCur,
                                                    Category = cc.CountryCurrencyCategoryId,
                                                    Status = cc.Status,
                                                    IsDefault = cc.Country3Iso.ToLower().Trim() == defaultCountryIso ? true : false
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
                var countries = await _unitOfWork.GetRepository<Country>().GetAll();

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
                            var response = new ApiResponse<List<GetCountryCurrencyResponse>>() { Success = false, Message = $"Invalid category: {c.Category}. Allowed categories are '1_Orig' and '1_Dest'." };
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

                var allowedCategories = new[] { "1_Orig", "1_Dest" };

                // Check if the category is valid (case-insensitive)
                if (!allowedCategories.Contains(model.Category.Trim(), StringComparer.OrdinalIgnoreCase))
                {
                    throw new ArgumentException($"Invalid category: {model.Category}. Allowed categories are '1_Orig' and '1_Dest'.");
                }


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

        // Define your Merchant class
        public class CountryLogoJson
        {
            public string CountryIso { get; set; }
            public string FlagUrl { get; set; }
        }
    }
}
