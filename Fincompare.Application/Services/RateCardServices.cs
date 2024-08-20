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

        public RateCardServices(IUnitOfWork unitOfWork, IMapper mapper, IMarketRateServices marketRateServices)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _marketRateServices = marketRateServices;
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


                var rateCardRequestModel = rateCards
                                                .Select(x =>
                                                {
                                                    var sendCurrency = x.Rate_Card.Substring(0, 3);
                                                    var receiveCurrency = x.Rate_Card.Substring(x.Rate_Card.Length - 3, 3);

                                                    var countryRate = countryRates
                                                        .FirstOrDefault(e => e.ReceiveCur == receiveCurrency && e.SendCur == sendCurrency);

                                                    return new RateCardRequestViewModel
                                                    {
                                                        SendCurrency3Iso = sendCurrency,
                                                        ReceiveCurrency3Iso = receiveCurrency,
                                                        Country3Iso = x.Country3Iso,
                                                        Rate = countryRate?.Rate ?? 0, // If no match found, set Rate to 0 or handle accordingly
                                                        Status = x.Status
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
            public string SendCurrency3Iso { get; set; }
            public string ReceiveCurrency3Iso { get; set; }
            public double Rate { get; set; }
            public bool Status { get; set; }
        }
    }
}

