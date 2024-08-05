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

        public async Task<ApiResponse<CreateInstrumentRequest>> CreateInstrument(CreateInstrumentRequest model)
        {
            try
            {
                if (model == null)
                    return new ApiResponse<CreateInstrumentRequest>() { Success = false, Message = "instrument creation failed" };
                var checkDuplication = (await _unitOfWork.GetRepository<Instrument>().GetAll())
                    .Where(x => x.Country3Iso == model.Country3Iso && x.InstrumentName.ToUpper().Trim() == model.InstrumentName.ToUpper().Trim())
                    .ToList();
                if (checkDuplication.Count > 0)
                    return new ApiResponse<CreateInstrumentRequest>() { Success = false, Message = model.InstrumentName + " +  record already exits", };
                var createInstrument = _mapper.Map<Instrument>(model);
                await _unitOfWork.GetRepository<Instrument>().Add(createInstrument);
                await _unitOfWork.SaveChangesAsync();
                var response = _mapper.Map<CreateInstrumentRequest>(createInstrument);
                return new ApiResponse<CreateInstrumentRequest>() { Success = true, Message = "instrument record created successfully", Data = response };

            }
            catch (Exception ex)
            {
                throw new ArgumentException($"instrument creation failed {ex.Message}");
            }
        }

        public async Task<ApiResponse<CreateInstrumentRequest>> UpdateInstrument(UpdateInstrumentRequest model)
        {
            try
            {
                var checkInstrument = await _unitOfWork.GetRepository<Instrument>().GetById(model.Id);
                if (checkInstrument == null)
                    return new ApiResponse<CreateInstrumentRequest>() { Success = false, Message = "instrument not found" };
                var updateDate = _mapper.Map(model, checkInstrument);
                await _unitOfWork.GetRepository<Instrument>().Upsert(updateDate);
                await _unitOfWork.SaveChangesAsync();
                var response = _mapper.Map<CreateInstrumentRequest>(updateDate);
                return new ApiResponse<CreateInstrumentRequest>() { Success = true, Message = "instrument record updated successfully", Data = response };
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"instrument update failed {ex.Message}");
            }
        }


        public async Task<ApiResponse<IEnumerable<GetAllInstrumentResponse>>> GetAllInstrument(int? idInstrument, bool? status, string? instrumentType, string? countryIso3)
        {
            try
            {
                var getInstrument = await _unitOfWork.GetRepository<Instrument>().GetAll();
                if (idInstrument.HasValue)
                {
                    getInstrument = getInstrument.Where(x => x.Id == idInstrument.Value);
                }
                if (status.HasValue)
                {
                    getInstrument = getInstrument.Where(x => x.Status == status.Value);
                }
                if (!string.IsNullOrEmpty(countryIso3))
                {
                    getInstrument = getInstrument.Where(x => x.Country3Iso == countryIso3);
                }
                if (!string.IsNullOrEmpty(instrumentType))
                {
                    getInstrument = getInstrument.Where(x => x.InstrumentType == instrumentType);
                }
                var getListOfAllInstrument = getInstrument
                    .Select(x => new GetAllInstrumentResponse
                    {
                        Id = x.Id,
                        InstrumentName = x.InstrumentName,
                        InstrumentType = x.InstrumentType,
                        Country3Iso = x.Country3Iso,
                        Status = x.Status
                    }).ToList();

                if (getListOfAllInstrument.Count == 0)
                    return new ApiResponse<IEnumerable<GetAllInstrumentResponse>>() { Success = false, Message = "instrument fetch failed" };
                return new ApiResponse<IEnumerable<GetAllInstrumentResponse>>() { Success = true, Message = "instrument record fetched successfully", Data = getListOfAllInstrument };
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
                    return new ApiResponse<GetAllInstrumentResponse>() { Success = false, Message = "Instrument Not Found!" };
                return new ApiResponse<GetAllInstrumentResponse>() { Success = true, Message = "Instrument Found!", Data = getData };
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
