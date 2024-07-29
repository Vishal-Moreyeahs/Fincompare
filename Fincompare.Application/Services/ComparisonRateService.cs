using Fincompare.Application.Repositories;

namespace Fincompare.Application.Services
{
    public class ComparisonRateService : IComparisonRateService
    {
        private readonly IMarketRateServices _marketRateServices;
        private readonly IMerchantRemitProductRateService _merchantRemitProductRateService;
        private readonly IMerchantRemitFee _merchantRemitFeeService;
        private readonly IMerchantProductService _merchantProductService;

        public ComparisonRateService(IMarketRateServices marketRateServices, IMerchantRemitProductRateService merchantRemitProductRateService, IMerchantProductService merchantProductService, IMerchantRemitFee merchantRemitFee)
        {
            _marketRateServices = marketRateServices;
            _merchantProductService = merchantProductService;
            _merchantRemitFeeService = merchantRemitFee;
            _merchantRemitProductRateService = merchantRemitProductRateService;
        }

        public async Task<List<MarkupCalculationResult>> GetMerchantRatesFromTable(string sendCountry, string receiveCountry, string sendCur, string receiveCur, double sendAmount, int? productId, int? serviceCategoryId, int? instrumentId)
        {
            var merchantProductTask = _merchantProductService.GetMerchantProducts(sendCountry, receiveCountry, sendCur, receiveCur, null, null, productId, serviceCategoryId, instrumentId, true);
            var merchantProductRateTask = _merchantRemitProductRateService.GetAllMerchantRemitProductRate(sendCountry, receiveCountry, sendCur, receiveCur, null, null, null, serviceCategoryId, instrumentId, sendAmount, null, true);
            var merchantProductFeeTask = _merchantRemitFeeService.GetMerchantRemittanceFee(sendCountry, receiveCountry, sendCur, receiveCur, null, null, null, serviceCategoryId, instrumentId, sendAmount, null, true, true);

            await Task.WhenAll(merchantProductTask, merchantProductRateTask, merchantProductFeeTask);

            var merchantProducts = merchantProductTask.Result.Data;
            if (!merchantProducts.Any())
            {
                throw new ApplicationException("Merchant Products not found");
            }

            var merchantProductRates = merchantProductRateTask.Result.Data;
            var merchantProductFees = merchantProductFeeTask.Result.Data;

            var joinedData = (from mp in merchantProducts
                              join mpr in merchantProductRates
                              on mp.Id equals mpr.MerchantProductId
                              join mpf in merchantProductFees
                              on mp.Id equals mpf.MerchantProductID
                              select new
                              {
                                  MerchantRate = mpr.Rate,
                                  MerchantPromoRate = mpr.PromoRate,
                                  MerchantRateRef = mpr.MerchantRateRef,
                                  MerchantTransferFee = mpf.Fees,
                                  MerchantTransferPromoFee = mpf.PromoFees,
                                  MerchantProductId = mp.Id,
                                  MerchantName = mp.MerchantName
                              }).ToList();

            var tasks = joinedData.Select(async merchantRate =>
            {
                return await CalculateMarkupAsync(sendCur, receiveCur, sendAmount, merchantRate.MerchantRate, merchantRate.MerchantTransferFee);
            });

            var list = await Task.WhenAll(tasks);
            return list.ToList();
        }

        public async Task<MarkupCalculationResult> CalculateMarkupAsync(string sendCur, string destCur, double sendAmount, double merchantRate, double merchantTransferFee)
        {
            if (string.IsNullOrWhiteSpace(sendCur))
            {
                throw new ArgumentException("Send currency cannot be null or empty", nameof(sendCur));
            }

            if (string.IsNullOrWhiteSpace(destCur))
            {
                throw new ArgumentException("Destination currency cannot be null or empty", nameof(destCur));
            }

            if (sendAmount <= 0)
            {
                throw new ArgumentException("Send amount must be greater than zero", nameof(sendAmount));
            }

            if (merchantRate <= 0)
            {
                throw new ArgumentException("Merchant rate must be greater than zero", nameof(merchantRate));
            }

            try
            {
                var data = await ConversionMidMarketRateForSourceAndDestCurrency(sendCur, destCur, sendAmount);

                var midMarketRate = data.MarketRate;

                // Total amount received using the mid-market rate
                var totalAmountMidMarket = data.ReceivedAmount;

                // Total amount received using the merchant rate
                var totalAmountReceived = merchantRate * sendAmount;


                // Additional cost
                var additionalCost = totalAmountMidMarket - totalAmountReceived;

                // Additional cost
                var additionalCostInSendCurr = additionalCost / merchantRate;

                var totalAdditionalCost = merchantTransferFee + additionalCostInSendCurr;

                // Markup percentage
                var markupPercentage = ((midMarketRate - merchantRate) / midMarketRate) * 100;

                var result = new MarkupCalculationResult
                {
                    TotalAmountReceived = totalAmountReceived,
                    AdditionalCost = additionalCost,
                    AdditionalCostInSendingCurrency = additionalCostInSendCurr,
                    TotalAdditionalCost = totalAdditionalCost,
                    TransferFee = merchantTransferFee,
                    MarkupPercentage = markupPercentage,
                    ProviderRate = merchantRate
                };

                return result;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                // Log the exception if necessary
                // Example: _logger.LogError(ex, "Error occurred while calculating markup");

                throw new InvalidOperationException("An error occurred while calculating the markup", ex);
            }
        }


        public async Task<ConversionRateViewModel> ConversionMidMarketRateForSourceAndDestCurrency(string sendCur, string destCur, double sendAmount)
        {
            if (string.IsNullOrWhiteSpace(sendCur))
            {
                throw new ArgumentException("Send currency cannot be null or empty", nameof(sendCur));
            }

            if (string.IsNullOrWhiteSpace(destCur))
            {
                throw new ArgumentException("Destination currency cannot be null or empty", nameof(destCur));
            }

            if (sendAmount <= 0)
            {
                throw new ArgumentException("Send amount must be greater than zero", nameof(sendAmount));
            }

            try
            {
                // Initiate both tasks in parallel
                var rateTask = _marketRateServices.GetMarketRateBySourceAndDestCurr(sendCur, destCur);

                // Wait for both tasks to complete
                var rate = await Task.WhenAll(rateTask);

                // Process the results
                var marketRate = rate[0]?.Data?.Rate ?? throw new Exception("Market rate data is null");

                var conversionRate = marketRate * sendAmount;
                var reverseMidMarketRate = 1 / marketRate;
                var reverseConversionRate = reverseMidMarketRate * sendAmount;

                var conversionRates = new ConversionRateViewModel
                {
                    SendCurrency = sendCur,
                    ReceiveCurrency = destCur,
                    ReceivedAmount = conversionRate,
                    MarketRate = marketRate,
                    ReverseReceivedAmount = reverseConversionRate
                };

                return conversionRates;
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException("An error occurred while fetching the conversion rates", ex);
            }
        }


        public class ConversionRateViewModel
        {
            public string SendCurrency { get; set; }
            public string ReceiveCurrency { get; set; }
            public double MarketRate { get; set; }

            public double ReceivedAmount { get; set; }
            public double ReverseReceivedAmount { get; set; }
        }

        public class MarkupCalculationResult
        {
            public double ProviderRate { get; set; }
            public double TotalAmountReceived { get; set; }
            public double AdditionalCost { get; set; }
            public double TransferFee { get; set; }
            public double TotalAdditionalCost { get; set; }
            public double AdditionalCostInSendingCurrency { get; set; }
            public double MarkupPercentage { get; set; }
        }
    }
}
