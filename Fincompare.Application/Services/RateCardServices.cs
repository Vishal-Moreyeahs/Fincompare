﻿using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
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
                var countryRates = await _marketRateServices.GetMarketRateBySendCurr(country3iso);
                var rateCardRequestModel = rateCards.Where(x => x.Country3Iso == country3iso)
                                            .Select(x => new RateCardRequestViewModel
                                            {
                                                SendCurrency3Iso = x.Rate_Card.Substring(0, 3),
                                                ReceiveCurrency3Iso = x.Rate_Card.Substring(x.Rate_Card.Length - 3, 3),
                                                Country3Iso = x.Country3Iso,
                                                Rate = countryRates.Data.Where(e => e.ReceiveCur == x.Rate_Card.Substring(x.Rate_Card.Length - 3, 3).ToString()).First().ReceiveCur,
                                                Status = x.Status
                                            }).ToList();

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
            public string Rate { get; set; }
            public bool Status { get; set; }
        }
    }
}

