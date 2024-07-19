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

        public async Task<ApiResponse<List<GetCountryCurrencyResponse>>> GetCurrenciesbyCountry3Iso(string country3Iso, string? categoryId)
        {
            try
            {
                var currencyList = _unitOfWork.GetRepository<CountryCurrency>().GetAllRelatedEntity();

                var currencies = new List<GetCountryCurrencyResponse>();

                if (categoryId == null)
                {
                    currencies = currencyList
                                            .Where(cc => cc.Country3Iso == country3Iso)
                                            .Select(cc => new GetCountryCurrencyResponse
                                            {
                                                CountryCurrencyID = cc.Id,
                                                CountryIso2 = cc.Country3IsoNavigation.Country2Iso,
                                                CountryIso3 = cc.Country3Iso,
                                                IsPrimary = cc.IsPrimaryCur,
                                                Category = cc.CountryCurrencyCategory.Definition,
                                                Status = cc.Status
                                            })
                                            .ToList();
                }
                else
                {
                    currencies = currencyList
                                                .Where(cc => cc.Country3Iso == country3Iso && cc.CountryCurrencyCategoryId == categoryId)
                                                .Select(cc => new GetCountryCurrencyResponse
                                                {
                                                    CountryCurrencyID = cc.Id,
                                                    CountryIso2 = cc.Country3IsoNavigation.Country2Iso,
                                                    CountryIso3 = cc.Country3Iso,
                                                    IsPrimary = cc.IsPrimaryCur,
                                                    Category = cc.CountryCurrencyCategory.Definition,
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

        public async Task<ApiResponse<string>> UpdateCountryWithMultipleCurrencies(UpdateCountryWithMultipleCurrencyRequest model)
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
                    IsPrimaryCur = c.IsPrimaryCur,
                    CountryCurrencyCategoryId = c.CountryCurrencyCategoryId,
                    //UpdatedDate = DateTime.UtcNow,
                    //CreatedDate = DateTime.UtcNow
                }).ToList();

                await _unitOfWork.GetRepository<CountryCurrency>().AddRange(newCountryCurrencies);
                await _unitOfWork.SaveChangesAsync();

                var response = new ApiResponse<string>()
                {
                    Status = true,
                    Message = $"Country Currencies Created successfully"
                };
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
