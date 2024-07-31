using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;
using static Fincompare.Application.Request.CurrencyRequest.CurrencyRequests;
using static Fincompare.Application.Response.CurrencyResponse.CurrencyResponseBaseModel;

namespace Fincompare.Application.Services
{
    public class CurrencyServices : ICurrencyServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CurrencyServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<Currency>> AddCurrency(AddCurrencyRequests model)
        {
            try
            {
                var checkCurrency = await _unitOfWork.GetRepository<Currency>().GetById(model.CurrencyIso);


                if (checkCurrency != null)
                    return new ApiResponse<Currency>() { Success = false, Message = "currency creation failed" };
                var currency = _mapper.Map<Currency>(model);
                await _unitOfWork.GetRepository<Currency>().Add(currency);
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse<Currency>()
                {
                    Success = true,
                    Message = "currency record created successfully",
                    Data = currency
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"currency creation failed {ex.Message}");
            }

        }

        public async Task<ApiResponse<Currency>> UpdateCurrency(UpdateCurrencyRequests model)
        {
            try
            {
                var checkCurrency = await _unitOfWork.GetRepository<Currency>().GetById(model.CurrencyIso);
                if (checkCurrency == null)
                    return new ApiResponse<Currency>()
                    {
                        Success = false,
                        Message = "currency update failed"
                    };

                var updateCurrency = _mapper.Map(model, checkCurrency);
                await _unitOfWork.GetRepository<Currency>().Upsert(updateCurrency);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<Currency>()
                {
                    Success = true,
                    Message = "currency record updated successfully",
                    Data = updateCurrency
                };
            }
            catch (Exception ex)
            {

                throw new ApplicationException($"currency update failed {ex.Message}");
            }
        }

        public async Task<ApiResponse<IEnumerable<GetAllCurrencyResponse>>> GetAllCurrency(string? country3Iso, string? currencyIso, bool? status)
        {
            try
            {
                var getAllCurrency = await _unitOfWork.GetRepository<Currency>().GetAll();

                var getData = getAllCurrency
                    .Select(x => new GetAllCurrencyResponse
                    {
                        CurrencyName = x.CurrencyName,
                        CurrencyIso = x.CurrencyIso,
                        Decimal = x.Decimal,
                        VolatilityRange = x.VolatilityRange,
                        Status = x.Status
                    }).ToList();

                if (!string.IsNullOrEmpty(country3Iso))
                {
                    var countryCurrencies = await _unitOfWork.GetRepository<CountryCurrency>().GetAll();
                    countryCurrencies = countryCurrencies.Where(x => x.Country3Iso == country3Iso);
                    var currencies = from c in getAllCurrency
                                     join cc in countryCurrencies
                                     on c.CurrencyIso equals cc.CurrencyIso
                                     select new GetAllCurrencyResponse
                                     {
                                         CurrencyName = c.CurrencyName,
                                         CurrencyIso = c.CurrencyIso,
                                         Decimal = c.Decimal,
                                         VolatilityRange = c.VolatilityRange,
                                         Status = c.Status
                                     };
                    getData = currencies.ToList();
                }

                if (!string.IsNullOrEmpty(currencyIso))
                    getData = getData.Where(x => x.CurrencyIso == currencyIso).ToList();


                if (status.HasValue)
                    getData = getData.Where(x => x.Status == status.Value).ToList();


                if (getData.Count > 0)
                    return new ApiResponse<IEnumerable<GetAllCurrencyResponse>>()
                    {
                        Success = true,
                        Message = "currency record fetched successfully",
                        Data = getData
                    };
                return new ApiResponse<IEnumerable<GetAllCurrencyResponse>>()
                {
                    Success = false,
                    Message = "currency fetch failed"
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"currency fetch failed {ex.Message}");
            }

        }

        public async Task<ApiResponse<GetCurrencyResponse>> GetByCurrencyId(string id)
        {
            var getAllCurrency = await _unitOfWork.GetRepository<Currency>().GetById(id);

            if (getAllCurrency == null)
                return new ApiResponse<GetCurrencyResponse>()
                {
                    Success = false,
                    Message = "Currency Not Found !",
                };
            var getData = _mapper.Map<GetCurrencyResponse>(getAllCurrency);
            return new ApiResponse<GetCurrencyResponse>()
            {
                Success = true,
                Message = "Currency Found !",
                Data = getData
            };
        }

        public async Task<ApiResponse<string>> DeleteCurrency(string id)
        {
            try
            {
                var checkCurrency = await _unitOfWork.GetRepository<Currency>().GetById(id);
                if (checkCurrency == null)
                    return new ApiResponse<string>() { Success = false, Message = "currency delete failed" };

                //checkCurrency.Status = false;
                checkCurrency.IsDeleted = true;
                await _unitOfWork.GetRepository<Currency>().Upsert(checkCurrency);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<string>() { Success = true, Message = "currency record deleted successfully" };
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"currency delete failed {ex.Message}");
            }
        }


    }
}
