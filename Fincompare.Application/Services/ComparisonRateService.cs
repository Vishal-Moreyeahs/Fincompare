using Fincompare.Application.Repositories;
using System.Text.RegularExpressions;

namespace Fincompare.Application.Services
{
    public class ComparisonRateService : IComparisonRateService
    {
        private readonly IMarketRateServices _marketRateServices;
        private readonly IMerchantRemitProductRateService _merchantRemitProductRateService;
        private readonly IMerchantRemitFee _merchantRemitFeeService;
        private readonly IMerchantProductService _merchantProductService;
        private readonly IMerchantServices _merchantServices;

        public ComparisonRateService(IMarketRateServices marketRateServices, IMerchantRemitProductRateService merchantRemitProductRateService, IMerchantProductService merchantProductService, IMerchantRemitFee merchantRemitFee, IMerchantServices merchantServices)
        {
            _marketRateServices = marketRateServices;
            _merchantProductService = merchantProductService;
            _merchantRemitFeeService = merchantRemitFee;
            _merchantRemitProductRateService = merchantRemitProductRateService;
            _merchantServices = merchantServices;
        }

        public async Task<List<MerchantDetails>> GetMerchantRatesFromTable(string sendCountry, string receiveCountry, string sendCur, string receiveCur, double sendAmount, double receiveAmount, int? productId, int? serviceCategoryId, int? instrumentId)
        {
            var merchantProductTask = await _merchantProductService.GetMerchantProducts(sendCountry, receiveCountry, sendCur, receiveCur, null, null, productId, serviceCategoryId, instrumentId, true);
            var merchantProductRateTask =await _merchantRemitProductRateService.GetAllMerchantRemitProductRate(sendCountry, receiveCountry, sendCur, receiveCur, null, null, null, serviceCategoryId, instrumentId, sendAmount, receiveAmount, true);
            var merchantProductFeeTask = await _merchantRemitFeeService.GetMerchantRemittanceFee(sendCountry, receiveCountry, sendCur, receiveCur, null, null, null, serviceCategoryId, instrumentId, sendAmount, receiveAmount, true, true);
            var merchantTask = await _merchantServices.GetAllMerchants(null,null,null,null,null);


            var merchantProducts = merchantProductTask.Data;
            if (!merchantProducts.Any())
            {
                throw new ApplicationException("Merchant Products not found");
            }

            var merchantProductRates = merchantProductRateTask.Data;
            var merchantProductFees = merchantProductFeeTask.Data;
            var merchants = merchantTask.Data;
            var midMarketData = await ConversionMidMarketRateForSourceAndDestCurrency(sendCur,receiveCur,sendAmount);


            var joinedData = (from mp in merchantProducts
                              join mpr in merchantProductRates
                              on mp.Id equals mpr.MerchantProductId
                              join mpf in merchantProductFees
                              on mp.Id equals mpf.MerchantProductID
                              join m in merchants
                              on mp.MerchantId equals m.Id
                              select new MerchantDetails
                              {
                                  MerchantId = mp.MerchantId,
                                  MerchantName = mp.MerchantName,
                                  AffiliateId = m.AffiliateId,
                                  Fees = mpf.Fees,
                                  FeesCurrency = mpf.FeesCurrency,
                                  PromoFees = mpf.Fees,
                                  VariableFee = mpf.VariableFee.HasValue ? mpf.VariableFee.Value : 0,
                                  MerchantLogo = "?",
                                  MerchantProductId = mp.Id,
                                  MerchantType = m.MerchantType,
                                  PayInInstrumentId = mpf.PayInInstrumentId.HasValue ? mpf.PayInInstrumentId.Value : 0,
                                  PayInInstrumentName = mpf.PayInInstrumentName,
                                  PayOutInstrumentId = mpf.InstrumentId,
                                  PayOutInstrumentName = mpf.InstrumentName,
                                  ProductId = mp.ProductId,
                                  ProductName = mp.ProductName,
                                  Rate = mpr.Rate,
                                  PromoRate = mpr.PromoRate,
                                  ReceiveCountry = mp.ReceiveCountry3Iso,
                                  SendCountry = mp.SendCountry3Iso,
                                  ReceiveCurrency = mp.ReceiveCurrencyId,
                                  SendCurrency = mp.SendCurrencyId,
                                  RoutingParameters = m.RoutingParameters,
                                  WebUrl = m.WebUrl,
                                  ServiceLevels = mp.ServiceLevels,
                                  TransferSpeed = ConvertServiceLevel(mp.ServiceLevels),
                                  TransferFee = CalculateCustomerTransactionFee(mpf.Fees,mpf.VariableFee.Value,sendAmount)
                              }).ToList();

            //var tasks = joinedData.Select(async merchantRate =>
            //{
            //    return await CalculateMarkupAsync(sendCur, receiveCur, sendAmount, merchantRate.MerchantRate, merchantRate.MerchantTransferFee);
            //});

            //var list = await Task.WhenAll(tasks);
            return joinedData;
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

                throw new InvalidOperationException("An error occurred while calculating the markup "+ ex.Message.ToString());
            }
        }

        public double CalculateCustomerTransactionFee(double productFee, double variableFeePercentage, double sendAmount)
        {
            double variableFee = (sendAmount - productFee) * (variableFeePercentage / 100);
            double customerTransactionFee = productFee + variableFee;
            return customerTransactionFee;
        }

        public string ConvertServiceLevel(string serviceLevel)
        {
            if (string.IsNullOrEmpty(serviceLevel))
                return string.Empty;

            var daysMatch = Regex.Match(serviceLevel, @"(\d+)D", RegexOptions.IgnoreCase);
            var hoursMatch = Regex.Match(serviceLevel, @"(\d+)H", RegexOptions.IgnoreCase);
            var minutesMatch = Regex.Match(serviceLevel, @"(\d+)M", RegexOptions.IgnoreCase);

            string result = string.Empty;

            if (daysMatch.Success)
            {
                int days = int.Parse(daysMatch.Groups[1].Value);
                result += days == 1 ? "1 Day" : $"{days} Days";
            }

            if (hoursMatch.Success)
            {
                int hours = int.Parse(hoursMatch.Groups[1].Value);
                if (!string.IsNullOrEmpty(result)) result += " ";
                result += hours == 1 ? "1 Hour" : $"{hours} Hours";
            }

            if (minutesMatch.Success)
            {
                int minutes = int.Parse(minutesMatch.Groups[1].Value);
                if (!string.IsNullOrEmpty(result)) result += " and ";
                result += minutes == 1 ? "1 Minute" : $"{minutes} Minutes";
            }

            return result;
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
        public class MerchantDetails
        {
            public int MerchantId { get; set; }
            public string MerchantName { get; set; }
            public string AffiliateId { get; set; }
            public string RoutingParameters { get; set; }
            public string WebUrl { get; set; }
            public string MerchantType { get; set; }
            public string MerchantLogo { get; set; }
            public int MerchantProductId { get; set; }
            public int PayOutInstrumentId { get; set; }
            public string PayOutInstrumentName { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string SendCountry { get; set; }
            public string ReceiveCountry { get; set; }
            public string SendCurrency { get; set; }
            public string ReceiveCurrency { get; set; }
            public string ServiceLevels { get; set; }
            public string FeesCurrency { get; set; }
            public double Fees { get; set; }
            public double PromoFees { get; set; }
            public double VariableFee { get; set; }
            public int PayInInstrumentId { get; set; }
            public string PayInInstrumentName { get; set; }
            public double Rate { get; set; }
            public double PromoRate { get; set; }
            
            public double TransferFee { get; set; }
            public string TransferSpeed { get; set; }

        }

    }
}
