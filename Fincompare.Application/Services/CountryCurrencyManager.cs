using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.CountryCurrencyRequests;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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

        public async Task<ApiResponse<List<GetCurrencyResponse>>> GetCurrenciesbyCountry3Iso(string country3Iso, int? categoryId)
        {
            try
            {
                var currencyList = _unitOfWork.GetRepository<CountryCurrency>().GetAllRelatedEntity();

                var currencies = new List<GetCurrencyResponse>();

                if (categoryId == null)
                {
                    currencies = currencyList
                                            .Where(cc => cc.Country3Iso == country3Iso && cc.Status && cc.Currency.Status)
                                            .Select(cc => new GetCurrencyResponse
                                            {
                                                Id = cc.Currency.Id,
                                                CurrencyName = cc.Currency.CurrencyName,
                                                CurrencyIso = cc.Currency.CurrencyIso,
                                                Decimal = cc.Currency.Decimal,
                                                VolatilityRange = cc.Currency.VolatilityRange,
                                                Status = cc.Currency.Status
                                            })
                                            .ToList();
                }
                else
                { 
                    currencies = currencyList
                                                .Where(cc => cc.Country3Iso == country3Iso && cc.Status && cc.Currency.Status && cc.CountryCurrencyCategoryId == categoryId)
                                                .Select(cc => new GetCurrencyResponse
                                                {
                                                    Id = cc.Currency.Id,
                                                    CurrencyName = cc.Currency.CurrencyName,
                                                    CurrencyIso = cc.Currency.CurrencyIso,
                                                    Decimal = cc.Currency.Decimal,
                                                    VolatilityRange = cc.Currency.VolatilityRange,
                                                    Status = cc.Currency.Status
                                                })
                                                .ToList();

                }
                var response = new ApiResponse<List<GetCurrencyResponse>>() {
                    Status = true,
                    Message = "Currencies fetched",
                    Data = currencies
                };

                return response;
            }
            catch (Exception ex) {
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
                    CurrencyId = c.Id,
                    IsPrimaryCur = c.IsPrimaryCur,
                    CountryCurrencyCategoryId = c.CountryCurrencyCategoryId,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow
                }).ToList();

                await _unitOfWork.GetRepository<CountryCurrency>().AddRange(newCountryCurrencies);
                await _unitOfWork.SaveChangesAsync();

                var response = new ApiResponse<string>() { 
                    Status = true,
                    Message = $"Update Currencies for Country {model.Country3Iso}"
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
