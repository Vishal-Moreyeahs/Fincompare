using AutoMapper;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;
using static Fincompare.Application.Request.InstrumentRequest.InstrumentRequestBaseModel;
using static Fincompare.Application.Response.InstrumentResponse.InstrumentResponseBaseClass;

namespace Fincompare.Application.Services
{
    public class InstrumentService : IInstrumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InstrumentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> CreateInstrument(CreateInstrumentRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<string>() { Status = false, Message = "Request Not Accepted !" };
                var createInstrument = _mapper.Map<Instrument>(model);
                await _unitOfWork.GetRepository<Instrument>().Add(createInstrument);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<string>() { Status = true, Message = "Instrument creation Successfully !" };

            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<ApiResponse<string>> UpdateInstrument(UpdateInstrumentRequest model)
        {
            try
            {
                var checkInstrument = await _unitOfWork.GetRepository<Instrument>().GetById(model.Id);
                if (checkInstrument == null)
                    return new ApiResponse<string>() { Status = false, Message = "Request Not Accepted" };
                var updateDate = _mapper.Map(model, checkInstrument);
                await _unitOfWork.GetRepository<Instrument>().Upsert(updateDate);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<string>() { Status = true, Message = "Instrument Updated Successfully !" };
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }


        public async Task<ApiResponse<IEnumerable<GetAllInstrumentResponse>>> GetAllInstrument()
        {
            try
            {
                var getInstrument = await _unitOfWork.GetRepository<Instrument>().GetAll();
                var getListOfAllInstrument = getInstrument
                    .Select(x => new GetAllInstrumentResponse
                    {
                        Id = x.Id,
                        InstrumentName = x.InstrumentName,
                        Country3Iso = x.Country3Iso,
                        Status = x.Status
                    }).ToList();
                if (getListOfAllInstrument.Count == 0)
                    return new ApiResponse<IEnumerable<GetAllInstrumentResponse>>() { Status = false, Message = "Instrument Not Found!" };
                return new ApiResponse<IEnumerable<GetAllInstrumentResponse>>() { Status = true, Message = "Instrument Found!", Data = getListOfAllInstrument };
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<ApiResponse<GetAllInstrumentResponse>> GetInstrumentById(int id)
        {
            try
            {
                var getInstrument = await _unitOfWork.GetRepository<Instrument>().GetById(id);
                var getData = _mapper.Map<GetAllInstrumentResponse>(getInstrument);
                if (getData == null)
                    return new ApiResponse<GetAllInstrumentResponse>() { Status = false, Message = "Instrument Not Found!" };
                return new ApiResponse<GetAllInstrumentResponse>() { Status = true, Message = "Instrument Found!", Data = getData };
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
