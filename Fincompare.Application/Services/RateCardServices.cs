using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.MarketRateRequest;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;

namespace Fincompare.Application.Services
{
    public class RateCardServices : IRateCardServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMarketRateServices _marketRateServices;
        private readonly ICountryCurrencyManager _countryCurrencyManager;
        

        public RateCardServices(IUnitOfWork unitOfWork, IMapper mapper, IMarketRateServices marketRateServices, ICountryCurrencyManager countryCurrencyManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _marketRateServices = marketRateServices;
            _countryCurrencyManager = countryCurrencyManager;
        }
        public async Task<ApiResponse<IEnumerable<RateCardRequestViewModel>>> GetRateCardByCountry3Iso(string country3iso)
        {
            try
            {
                var rateCards = await _unitOfWork.GetRepository<RateCard>().GetAll();
                rateCards = rateCards.Where(x => x.Country3Iso == country3iso).ToList();
                var rateCardCurrencies = rateCards.Select(x => x.Rate_Card.Substring(0, 3)).Distinct().ToList<string>();
                var countryRates = new List<MarketRateDto>();
                foreach (var currency in rateCardCurrencies)
                {
                    var rates = await _marketRateServices.GetMarketRateBySendCurr(currency);
                    if(rates.Success)
                        countryRates.AddRange(rates.Data);
                }

                var countryCurrencies = await _countryCurrencyManager.GetCurrenciesbyCountry3Iso(null, null, null);

                if (!countryCurrencies.Success)
                {
                    throw new ApplicationException("Country Currencies not found");
                }

                var countryWithCurrencies = countryCurrencies.Data.ToList();

                var rateCardRequestModel = rateCards
                                                 .Select(x =>
                                                 {
                                                     var sendCurrency = x.Rate_Card.Substring(0, 3);
                                                     var receiveCurrency = x.Rate_Card.Substring(x.Rate_Card.Length - 3, 3);

                                                     // Find the matching country for sending and receiving currencies
                                                     var sendCountry = countryWithCurrencies
                                                         .FirstOrDefault(curr => curr.CurrencyIso == sendCurrency);
                                                     var receiveCountry = countryWithCurrencies
                                                         .FirstOrDefault(curr => curr.CurrencyIso == receiveCurrency);

                                                     // Find the rate for the corresponding send and receive currencies
                                                     var countryRate = countryRates
                                                         .FirstOrDefault(e => e.ReceiveCur == receiveCurrency && e.SendCur == sendCurrency);

                                                     return new RateCardRequestViewModel
                                                     {
                                                         SendCurrency3Iso = sendCurrency,
                                                         ReceiveCurrency3Iso = receiveCurrency,
                                                         Country3Iso = x.Country3Iso,
                                                         Rate = countryRate?.Rate ?? 0, // If no match found, set Rate to 0
                                                         Status = x.Status,
                                                         // Get Send and Receive Country name and flag
                                                         SendCountryName = sendCountry?.CountryName ?? "Unknown",
                                                         SendCountryFlag = sendCountry?.CountryFlag ?? "Unknown",
                                                         ReceiveCountryName = receiveCountry?.CountryName ?? "Unknown",
                                                         ReceiveCountryFlag = receiveCountry?.CountryFlag ?? "Unknown"
                                                     };
                                                 })
                                                 .ToList();




                if (rateCardRequestModel == null || rateCardRequestModel.Count == 0)
                {
                    return new ApiResponse<IEnumerable<RateCardRequestViewModel>>()
                    {
                        Success = false,
                        Message = " Rate Card fetch failed"
                    };
                }

                return new ApiResponse<IEnumerable<RateCardRequestViewModel>>()
                {
                    Success = true,
                    Message = " Rate Card record fetched successfully",
                    Data = rateCardRequestModel
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Rate card rates fetch failed{ex.Message}");
            }

        }

        public class RateCardRequestViewModel
        {
            public string Country3Iso { get; set; }
            public string SendCountryFlag { get; set; }
            public string SendCountryName { get; set; }
            public string ReceiveCountryFlag { get; set; }
            public string ReceiveCountryName { get; set; }
            public string SendCurrency3Iso { get; set; }
            public string ReceiveCurrency3Iso { get; set; }
            public double Rate { get; set; }
            public string Type { get; set; } = "locked";
            public bool Status { get; set; }
        }
    }
}

