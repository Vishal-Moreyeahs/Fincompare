using Fincompare.Application.Response;
using static Fincompare.Application.Request.InstrumentRequest.InstrumentRequestBaseModel;
using static Fincompare.Application.Response.InstrumentResponse.InstrumentResponseBaseClass;

namespace Fincompare.Application.Repositories
{
    public interface IInstrumentService
    {
        Task<ApiResponse<string>> CreateInstrument(CreateInstrumentRequest model);
        Task<ApiResponse<string>> UpdateInstrument(UpdateInstrumentRequest model);
        Task<ApiResponse<IEnumerable<GetAllInstrumentResponse>>> GetAllInstrument();
        Task<ApiResponse<GetAllInstrumentResponse>> GetInstrumentById(int id);
    }
}
