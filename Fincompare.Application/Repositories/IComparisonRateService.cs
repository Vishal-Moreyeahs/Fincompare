using static Fincompare.Application.Services.ComparisonRateService;

namespace Fincompare.Application.Repositories
{
    public interface IComparisonRateService
    {
        Task<List<MerchantDetails>> GetMerchantRatesFromTable(string sendCountry, string receiveCountry, string sendCur, string receiveCur, double sendAmount, /*double receiveAmount,*/int? productId, int? serviceCategoryId, int? instrumentId);
        Task<MarkupCalculationResult> CalculateMarkupAsync(string sendCur, string destCur, double sendAmount, double merchantRate, double merchantTransferFee);
        Task<ConversionRateViewModel> ConversionMidMarketRateForSourceAndDestCurrency(string sendCur, string destCur, double sendAmount);
    }
}
