using Fincompare.Application.Response;
using Fincompare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fincompare.Application.Services.RateCardServices;

namespace Fincompare.Application.Repositories
{
    public interface IRateCardServices
    {
        Task<ApiResponse<IEnumerable<RateCardRequestViewModel>>> GetRateCardByCountry3Iso(string country3iso);
    }
}
