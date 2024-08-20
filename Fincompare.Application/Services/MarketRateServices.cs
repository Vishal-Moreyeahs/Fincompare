using AutoMapper;
using Fincompare.Application.Contracts.Infrastructure;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.MarketRateRequest;
using Fincompare.Application.Response;
using Fincompare.Application.Response.MarketRateResponse;
using Fincompare.Domain.Entities;
namespace Fincompare.Application.Services
{
    public class MarketRateServices : IMarketRateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrencyServices _currencyServices;
        private readonly IExchangeRate _exchangeRate;
        public MarketRateServices(IUnitOfWork unitOfWork, IMapper mapper, ICurrencyServices currencyServices, IExchangeRate exchangeRate)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currencyServices = currencyServices;
            _exchangeRate = exchangeRate;
        }

        public async Task<ApiResponse<string>> AddMarketRate(List<AddMarketRate> model)
        {
            // Validate request

            try
            {
                if (model.Count == 0)
                    return new ApiResponse<string>() { Success = false, Data = "Mid-market rate creation failed" };

                //var sendCurrency = await _unitOfWork.GetRepository<Currency>().GetById(model.SendCur);

                //if (sendCurrency == null)
                //{
                //    throw new ArgumentException("Invalid Send Currency.");
                //}

                //var receiveCurrency = await _unitOfWork.GetRepository<Currency>().GetById(model.ReceiveCur);
                //if (receiveCurrency == null)
                //{
                //    throw new ArgumentException("Invalid Receive Currency.");
                //}

                // Map request to entity
                var marketRate = _mapper.Map<List<MarketRate>>(model);
                // Add to database
                await _unitOfWork.GetRepository<MarketRate>().AddRange(marketRate);

                // Save changes asynchronously
                await _unitOfWork.SaveChangesAsync();

                var response = new ApiResponse<string>()
                {
                    Success = true,
                    Message = "Mid-market rate record created successfully"
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Mid-market rate creation failed {ex.Message}");
            }
            // Validate foreign keys

        }

        public async Task<ApiResponse<IEnumerable<MarketRateDto>>> GetAllMarketRates()
        {
            try
            {
                var marketRates = await _unitOfWork.GetRepository<MarketRate>().GetAll();

                if (marketRates == null || marketRates.ToList().Count == 0)
                {
                    throw new ApplicationException("Mid-Market Rates fetch failed");
                }

                var data = _mapper.Map<IEnumerable<MarketRateDto>>(marketRates);

                if (data == null || data.ToList().Count == 0)
                {
                    return new ApiResponse<IEnumerable<MarketRateDto>>()
                    {
                        Success = false,
                        Message = "Mid-Market rate fetch failed"
                    };
                }

                var response = new ApiResponse<IEnumerable<MarketRateDto>>()
                {
                    Success = true,
                    Message = "Mid-Market rate record fetched successfully",
                    Data = data
                };
                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Mid-market rate fetch failed {ex.Message}");
            }

        }

        public async Task<MarketRateStatisticsData> GetAllMarketRatesStatistics(string sendCurr, string receiveCurr)
        {
            var lastWeekRates = DateTime.UtcNow.AddDays(-7);

            var lastMonthRates = DateTime.UtcNow.AddMonths(-1);
            var last2MonthRates = DateTime.UtcNow.AddMonths(-2);


            //Get List of Rates 
            var lastWeekRateList = new List<MarketRateStatisticsData>();
            var lastMonthRateList = new List<MarketRateStatisticsData>();
            var last2MonthRateList = new List<MarketRateStatisticsData>();


            var getAllMidMarketRates = await _unitOfWork.GetRepository<MarketRate>().GetAll();
            var getRateForSendAndDesCurr = getAllMidMarketRates.Where(x => x.SendCur == sendCurr && x.ReceiveCur == receiveCurr).ToList();
            var getLastWeekRates = getRateForSendAndDesCurr.Where(x => x.Date <= lastWeekRates);
            var getLastMonthRates = getRateForSendAndDesCurr.Where(x => x.Date <= lastMonthRates);
            var getLast2MonthRates = getRateForSendAndDesCurr.Where(x => x.Date <= lastWeekRates);


            //var response = new MarketRateStatisticsData()
            //{
            //    RateWeek = 
            //};

            throw new NotImplementedException();
        }

        public async Task<ApiResponse<MarketRateDto>> GetMarketRateById(int id)
        {
            try
            {
                var marketRate = await _unitOfWork.GetRepository<MarketRate>().GetById(id);

                if (marketRate == null)
                {
                    throw new ApplicationException("Mid-Market rate fetch failed");
                }

                var data = _mapper.Map<MarketRateDto>(marketRate);

                var response = new ApiResponse<MarketRateDto>()
                {
                    Success = true,
                    Message = "Mid-Market Rate record fetched successfully",
                    Data = data
                };
                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Mid-market rate fetch failed {ex.Message}");
            }

        }

        public async Task<ApiResponse<List<MarketRateDto>>> GetMarketRateBySendCurr(string sendCurr)
        {
            var response = new ApiResponse<List<MarketRateDto>>();
            var oneHourAgo = DateTime.UtcNow.AddHours(-1);

            if (string.IsNullOrEmpty(sendCurr))
            {
                response.Success = false;
                response.Message = "Send currency is required.";
                return response;
            }

            try
            {
                // Filter data at the database level
                var marketRates = await _unitOfWork.GetRepository<MarketRate>()
                                                       .GetAll();

                var marketCurrRates = marketRates.ToList()
                            .Where(mr => mr.SendCur == sendCurr).ToList();

                var mark = marketCurrRates.GroupBy(mr => new { mr.SendCur, mr.ReceiveCur })
                            .Select(g => g.OrderByDescending(mr => mr.Date).FirstOrDefault())
                            .ToList();


                if (marketCurrRates == null || !marketCurrRates.Any())
                {
                    response.Success = false;
                    response.Message = "No market rates found for the specified send currency.";
                    return response;
                }

                // Map the filtered data to the DTO
                var data = _mapper.Map<List<MarketRateDto>>(mark);
                if (data == null || data.ToList().Count == 0)
                {
                    return new ApiResponse<List<MarketRateDto>>()
                    {
                        Success = false,
                        Message = "Mid-Market rate fetch failed"
                    };
                }
                response.Success = true;
                response.Message = "Market Rate record fetched successfully";
                response.Data = data;
            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your logging framework)

                response.Success = false;
                response.Message = "Mid-Market rate fetch failed";
            }

            return response;
        }


        public async Task<ApiResponse<MarketRateDto>> GetMarketRateBySourceAndDestCurr(string sourceCurr, string destCurr)
        {
            var response = new ApiResponse<MarketRateDto>();
            var oneHourAgo = DateTime.UtcNow.AddHours(-1);

            if (string.IsNullOrEmpty(sourceCurr) || string.IsNullOrEmpty(destCurr))
            {
                response.Success = false;
                response.Message = "Send and Receive currency is required.";
                return response;
            }

            try
            {
                // Filter data at the database level
                var marketRates = await _unitOfWork.GetRepository<MarketRate>()
                                                       .GetAll();

                var marketCurrRates = marketRates.Where(x => x.SendCur == sourceCurr && x.ReceiveCur == destCurr).OrderByDescending(x => x.Date)
                                                       .FirstOrDefault();

                if (marketCurrRates == null)
                {
                    response.Success = false;
                    response.Message = "No market rates found for the specified send and receive currency.";
                    return response;
                }

                // Map the filtered data to the DTO
                var data = _mapper.Map<MarketRateDto>(marketCurrRates);

                response.Success = true;
                response.Message = "Market Rate record fetched successfully";
                response.Data = data;
            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your logging framework)

                response.Success = false;
                response.Message = "Mid-Market rate fetch failed";
            }

            return response;
        }


        public async Task<ApiResponse<List<string>>> UpdateDbCurrencyExchangeRates()
        {
            List<string> failCurrency = [];
            try
            {
                var getAllCurrencyCode = (await _currencyServices.GetAllCurrency(null, null, null))
                    .Data.Select(x => x.CurrencyIso).OrderBy(x => x).ToArray();
                foreach (var currencyCode in getAllCurrencyCode)
                {
                    DateTime currentDateTime = DateTime.UtcNow;
                    var conversionData = _exchangeRate.Import(currencyCode);
                    if (conversionData == null)
                    {
                        failCurrency.Add(currencyCode);
                        continue;
                    }
                    var addToDb = conversionData.conversion_rates
                        .Where(x => getAllCurrencyCode.Contains(x.Key))
                        .Select(x => new MarketRate
                        {
                            SendCur = currencyCode,
                            ReceiveCur = x.Key,
                            Rate = x.Value,
                            Date = currentDateTime,
                            RateSource = "Third-Party",
                        })
                        .ToList();

                    await _unitOfWork.GetRepository<MarketRate>().AddRange(addToDb);
                    await _unitOfWork.SaveChangesAsync();
                }
                if (failCurrency.Count != 0)
                    return new ApiResponse<List<string>>()
                    {
                        Message = "Some Currency Not Update",
                        Data = failCurrency,
                    };
                return new ApiResponse<List<string>>()
                {
                    Message = "Success",
                    Success = true,
                    Data = failCurrency,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
