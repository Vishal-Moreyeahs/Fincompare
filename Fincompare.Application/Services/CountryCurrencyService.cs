using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;
using static Fincompare.Application.Request.CountryCurrencyRequest.CountryCurrencyBaseModel;
using static Fincompare.Application.Response.CountryCurrencyResponse.CountryCurrencyResponseBaseClass;

namespace Fincompare.Application.Services
{
    public class CountryCurrencyService : ICountryCurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryCurrencyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ApiResponse<string>> AddCountryCurrency(AddCountryCurrencyRequest model)
        {
            try
            {
                var countryCurrency = _mapper.Map<CountryCurrency>(model);
                await _unitOfWork.GetRepository<CountryCurrency>().Add(countryCurrency);
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse<string>()
                {
                    Status = true,
                    Message = "Country Currency Created Successfully !"
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ApiResponse<string>> UpdateCountryCurrency(UpdateCountryCurrency model)
        {
            try
            {
                var checkCountryCurrency = await _unitOfWork.GetRepository<CountryCurrency>().GetById(model.Id);
                if (checkCountryCurrency == null)
                    return new ApiResponse<string>() { Status = false, Message = "Country Currency Not Found !" };
                var updateCountryCurrency = _mapper.Map(model, checkCountryCurrency);
                await _unitOfWork.GetRepository<CountryCurrency>().Upsert(updateCountryCurrency);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<string>() { Status = true, Message = "Country Currency Update Successfully !" };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ApiResponse<IEnumerable<GetAllCountryCurrencyResponse>>> GetAllCountryCurrency()
        {
            try
            {
                var getAllData = await _unitOfWork.GetRepository<CountryCurrency>().GetAll();
                var getAllCountryCurrency = getAllData
                    .Select(x => new GetAllCountryCurrencyResponse
                    {
                        Id = x.Id,
                        Country3Iso = x.Country3Iso,
                        CurrencyId = x.CurrencyId,
                        IsPrimaryCur = x.IsPrimaryCur,
                        CountryCurrencyCategoryId = x.CountryCurrencyCategoryId,
                        Status = x.Status
                    }).ToList();
                if (getAllCountryCurrency.Count > 0)
                    return new ApiResponse<IEnumerable<GetAllCountryCurrencyResponse>>()
                    {
                        Status = true,
                        Message = "Country Currency Found!",
                        Data = getAllCountryCurrency
                    };
                return new ApiResponse<IEnumerable<GetAllCountryCurrencyResponse>>()
                {
                    Status = false,
                    Message = "Country Currency Not Found!"
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ApiResponse<GetAllCountryCurrencyResponse>> GetCountryCurrencyById(int id)
        {
            try
            {
                var getCountryCurrency = await _unitOfWork.GetRepository<CountryCurrency>().GetById(id);
                if (getCountryCurrency == null)
                    return new ApiResponse<GetAllCountryCurrencyResponse>()
                    {
                        Status = false,
                        Message = "Country Currency Not Found !",
                    };
                var getData = _mapper.Map<GetAllCountryCurrencyResponse>(getCountryCurrency);
                return new ApiResponse<GetAllCountryCurrencyResponse>()
                {
                    Status = true,
                    Message = "Country Currency Found !",
                    Data = getData,
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ApiResponse<string>> DeleteCountryCurrency(int id)
        {
            try
            {
                var getDataForDelete = await _unitOfWork.GetRepository<CountryCurrency>().GetById(id);
                if (getDataForDelete == null)
                    return new ApiResponse<string>() { Status = false, Message = "Country Currency Not Found !" };
                await _unitOfWork.GetRepository<CountryCurrency>().Delete(getDataForDelete);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<string>() { Status = true, Message = "Country Currency Deleted Successfully !" };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
