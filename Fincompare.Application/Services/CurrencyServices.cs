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

        public async Task<ApiResponse<string>> AddCurrency(AddCurrencyRequests model)
        {
            try
            {
                var currency = _mapper.Map<Currency>(model);
                await _unitOfWork.GetRepository<Currency>().Add(currency);
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse<string>()
                {
                    Status = true,
                    Message = "Currency Added Successfully !",
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<ApiResponse<string>> UpdateCurrency(UpdateCurrencyRequests model)
        {
            try
            {
                var checkCurrency = await _unitOfWork.GetRepository<Currency>().GetById(model.Id);
                if (checkCurrency == null)
                    return new ApiResponse<string>()
                    {
                        Status = false,
                        Message = "Currency Not Found !"
                    };

                var updateCurrency = _mapper.Map(model, checkCurrency);
                await _unitOfWork.GetRepository<Currency>().Upsert(updateCurrency);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<string>()
                {
                    Status = true,
                    Message = "Currency Update Successfully !",
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<ApiResponse<IEnumerable<GetAllCurrencyResponse>>> GetAllCurrency()
        {
            try
            {
                var getAllCurrency = await _unitOfWork.GetRepository<Currency>().GetAll();
                var getData = getAllCurrency
                    .Select(x => new GetAllCurrencyResponse
                    {
                        Id = x.Id,
                        CurrencyName = x.CurrencyName,
                        CurrencyIso = x.CurrencyIso,
                        Decimal = x.Decimal,
                        Status = x.Status,
                        VolatilityRange = x.VolatilityRange,
                    }).ToList();

                if (getData.Count > 0)
                    return new ApiResponse<IEnumerable<GetAllCurrencyResponse>>()
                    {
                        Status = false,
                        Message = "Currency Not Found !",
                        Data = getData
                    };
                return new ApiResponse<IEnumerable<GetAllCurrencyResponse>>()
                {
                    Status = true,
                    Message = "Currency Found !",
                    Data = getData
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<ApiResponse<GetCurrencyResponse>> GetByCurrencyId(int id)
        {
            var getAllCurrency = await _unitOfWork.GetRepository<Currency>().GetById(id);

            if (getAllCurrency == null)
                return new ApiResponse<GetCurrencyResponse>()
                {
                    Status = false,
                    Message = "Currency Not Found !",
                };
            var getData = _mapper.Map<GetCurrencyResponse>(getAllCurrency);
            return new ApiResponse<GetCurrencyResponse>()
            {
                Status = true,
                Message = "Currency Found !",
                Data = getData
            };
        }

        public async Task<ApiResponse<string>> DeleteCurrency(int id)
        {
            try
            {
                var checkCurrency = await _unitOfWork.GetRepository<Currency>().GetById(id);
                if (checkCurrency == null)
                    return new ApiResponse<string>() { Status = false, Message = "Currency Not Found !" };

                checkCurrency.Status = false;
                await _unitOfWork.GetRepository<Currency>().Upsert(checkCurrency);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<string>() { Status = true, Message = "Currency Deleted Successfully !" };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
