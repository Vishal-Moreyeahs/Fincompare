using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.MarketRateRequest;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Services
{
    public class MarketRateServices : IMarketRateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MarketRateServices()
        {
        }

        public MarketRateServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> AddMarketRate(AddMarketRate model)
        {
            // Validate request
            if (model.SendCur == null || model.ReceiveCur == null)
            {
                throw new ArgumentException("Currency values must be greater than zero.");
            }

            if (model.Rate <= 0)
            {
                throw new ArgumentException("Rate must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(model.RateSource))
            {
                throw new ArgumentException("Rate source must be provided.");
            }

            // Validate foreign keys
            var sendCurrency = await _unitOfWork.GetRepository<Currency>().GetByStringId(model.SendCur);

            if (sendCurrency == null)
            {
                throw new ArgumentException("Invalid Send Currency.");
            }

            var receiveCurrency = await _unitOfWork.GetRepository<Currency>().GetByStringId(model.ReceiveCur);
            if (receiveCurrency == null)
            {
                throw new ArgumentException("Invalid Receive Currency.");
            }

            // Map request to entity
            var marketRate = _mapper.Map<MarketRate>(model);
            marketRate.Date = DateTime.UtcNow;
            // Add to database
            await _unitOfWork.GetRepository<MarketRate>().Add(marketRate);

            // Save changes asynchronously
            await _unitOfWork.SaveChangesAsync();

            var response = new ApiResponse<string>() { 
                Status = true,
                Message = "Market Rate Added Successfully."
            };

            return response;
        }

        public async Task<ApiResponse<IEnumerable<MarketRateDto>>> GetAllMarketRates()
        {
            var marketRates = await _unitOfWork.GetRepository<MarketRate>().GetAll();

            if (marketRates == null || marketRates.ToList().Count == 0)
            {
                throw new ApplicationException("Market Rates not found");
            }

            var data = _mapper.Map<IEnumerable<MarketRateDto>>(marketRates);

            var response = new ApiResponse<IEnumerable<MarketRateDto>>() { 
                Status = true,
                Message = "Market Rates Fetched",
                Data = data
            };
            return response;
        }

        public async Task<ApiResponse<MarketRateDto>> GetMarketRateById(int id)
        {
            var marketRate = await _unitOfWork.GetRepository<MarketRate>().GetById(id);

            if (marketRate == null)
            {
                throw new ApplicationException("Market Rate not found");
            }

            var data = _mapper.Map<MarketRateDto>(marketRate);

            var response = new ApiResponse<MarketRateDto>()
            {
                Status = true,
                Message = "Market Rate Fetched",
                Data = data
            };
            return response;
        }

        public async Task<ApiResponse<List<MarketRateDto>>> GetMarketRateBySendCurr(string sendCurr)
        {
            var response = new ApiResponse<List<MarketRateDto>>();

            if (string.IsNullOrEmpty(sendCurr))
            {
                response.Status = false;
                response.Message = "Send currency is required.";
                return response;
            }

            try
            {
                // Filter data at the database level
                var marketRates = await _unitOfWork.GetRepository<MarketRate>()
                                                       .GetAll();

                var marketCurrRates = marketRates.Where(x => x.SendCur == sendCurr)
                                                       .ToList();

                if (marketCurrRates == null || !marketCurrRates.Any())
                {
                    response.Status = false;
                    response.Message = "No market rates found for the specified send currency.";
                    return response;
                }

                // Map the filtered data to the DTO
                var data = _mapper.Map<List<MarketRateDto>>(marketCurrRates);

                response.Status = true;
                response.Message = "Market Rate Fetched";
                response.Data = data;
            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your logging framework)

                response.Status = false;
                response.Message = "An error occurred while fetching market rates.";
            }

            return response;
        }


        public async Task<ApiResponse<MarketRateDto>> GetMarketRateBySourceAndDestCurr(string sourceCurr, string destCurr)
        {
            var response = new ApiResponse<MarketRateDto>();

            if (string.IsNullOrEmpty(sourceCurr) || string.IsNullOrEmpty(destCurr))
            {
                response.Status = false;
                response.Message = "Send and Receive currency is required.";
                return response;
            }

            try
            {
                // Filter data at the database level
                var marketRates = await _unitOfWork.GetRepository<MarketRate>()
                                                       .GetAll();

                var marketCurrRates = marketRates.Where(x => x.SendCur == sourceCurr && x.ReceiveCur == destCurr)
                                                       .FirstOrDefault();

                if (marketCurrRates == null)
                {
                    response.Status = false;
                    response.Message = "No market rates found for the specified send and receive currency.";
                    return response;
                }

                // Map the filtered data to the DTO
                var data = _mapper.Map<MarketRateDto>(marketCurrRates);

                response.Status = true;
                response.Message = "Market Rate Fetched";
                response.Data = data;
            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your logging framework)

                response.Status = false;
                response.Message = "An error occurred while fetching market rates.";
            }

            return response;
        }
    }
}
