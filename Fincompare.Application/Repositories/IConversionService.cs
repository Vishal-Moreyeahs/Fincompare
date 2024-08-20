using Fincompare.Application.Response;
using Fincompare.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Repositories
{
    public interface IConversionService
    {
        Task<ApiResponse<ConversionResponseViewModel>> GetConversionResult(string sendCurrency, string receiveCurrency, double sendAmount);
    }
}
