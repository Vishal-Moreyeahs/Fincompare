using Fincompare.Application.Response;
using static Fincompare.Application.Request.InstrumentRequest.InstrumentRequestBaseModel;
using static Fincompare.Application.Response.InstrumentResponse.InstrumentResponseBaseClass;
using static Fincompare.Application.Services.InstrumentService;

namespace Fincompare.Application.Repositories
{
    public interface IInstrumentService
    {
        Task<ApiResponse<CreateInstrumentRequest>> CreateInstrument(CreateInstrumentRequest model);
        Task<ApiResponse<CreateInstrumentRequest>> UpdateInstrument(UpdateInstrumentRequest model);
        Task<ApiResponse<IEnumerable<GetAllInstrumentResponse>>> GetAllInstrument(int? idInstrument, bool? status, string? instrumentType/*, string? countryIso3*/);
        Task<ApiResponse<IEnumerable<PayoutInstrumentModel>>> GetAllPayoutInstrument(string sendCountry,
                                                            string receiveCountry,
                                                            string sendCurrency,
                                                            string receiveCurrency,
                                                            int? serviceCategoryId,
                                                            int? productId);
        Task<ApiResponse<GetAllInstrumentResponse>> GetInstrumentById(int id);
    }
}
