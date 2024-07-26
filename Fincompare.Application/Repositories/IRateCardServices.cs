using Fincompare.Application.Response;
using static Fincompare.Application.Services.RateCardServices;

namespace Fincompare.Application.Repositories
{
    public interface IRateCardServices
    {
        Task<ApiResponse<IEnumerable<RateCardRequestViewModel>>> GetRateCardByCountry3Iso(string country3iso);
    }
}
