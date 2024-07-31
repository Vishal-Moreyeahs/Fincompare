using static Fincompare.Application.Response.ComparisonResponse.ComparisonResponseViewModel;
using static Fincompare.Application.Services.ComparisonRateService;

namespace Fincompare.Application.Repositories
{
    public interface IComparisonRateService
    {
        Task<List<MerchantProductComparisonDto>> GetMerchantRatesFromTable(
                    string sendCountry3Iso,
                    string receiveCountry3Iso,
                    string sendCurrencyId,
                    string receiveCurrencyId,
                    double sendAmount,/* double receiveAmount,*/
                    int? productId,
                    int? serviceCategoryId,
                    int? instrumentId);
        Task<MarkupCalculationResult> CalculateMarkupAsync(string sendCur, string destCur, double sendAmount, double merchantRate, double merchantTransferFee);
        Task<ConversionRateViewModel> ConversionMidMarketRateForSourceAndDestCurrency(string sendCur, string destCur, double sendAmount);
    }
}
