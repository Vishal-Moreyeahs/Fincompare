using Fincompare.Application.Repositories;
using Fincompare.Application.Response;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Text.RegularExpressions;
using static Fincompare.Application.Response.ComparisonResponse.ComparisonResponseViewModel;

namespace Fincompare.Application.Services
{
    public class ComparisonRateService : IComparisonRateService
    {
        private readonly IMarketRateServices _marketRateServices;
        private readonly IMerchantRemitProductRateService _merchantRemitProductRateService;
        private readonly IMerchantRemitFee _merchantRemitFeeService;
        private readonly IMerchantProductService _merchantProductService;
        private readonly IMerchantServices _merchantServices;
        private readonly IActiveAssetService _activeAssets;
        private readonly IConfiguration _configuration;
        public ComparisonRateService(IConfiguration configuration, IActiveAssetService activeAssets, IMarketRateServices marketRateServices, IMerchantRemitProductRateService merchantRemitProductRateService, IMerchantProductService merchantProductService, IMerchantRemitFee merchantRemitFee, IMerchantServices merchantServices)
        {
            _marketRateServices = marketRateServices;
            _merchantProductService = merchantProductService;
            _merchantRemitFeeService = merchantRemitFee;
            _merchantRemitProductRateService = merchantRemitProductRateService;
            _merchantServices = merchantServices;
            _activeAssets = activeAssets;
            _configuration = configuration;
        }

        public async Task<ApiResponse<List<MerchantProductComparisonDto>>> GetMerchantRatesFromTable(
            string sendCountry3Iso,
            string receiveCountry3Iso,
            string sendCurrencyId,
            string receiveCurrencyId,
            double sendAmount,/* double receiveAmount,*/
            int? productId,
            int? serviceCategoryId,
            int? instrumentId)
        {

            var response = new ApiResponse<List<MerchantProductComparisonDto>>();
            var data = await GetDataFromPostgreFunction(sendCountry3Iso, receiveCountry3Iso, sendCurrencyId, receiveCurrencyId, sendAmount, productId, serviceCategoryId, instrumentId);

            if (data == null || data.Count ==0)
            {
                response.Message = "comparison fetch failed";
            }

            response.Success = true;
            response.Message = "comparison record fetched successfully";
            response.Data = data;
            return response;
        }


        private async Task<List<MerchantProductComparisonDto>> GetDataFromPostgreFunction(string sendCountry3Iso,
            string receiveCountry3Iso,
            string sendCurrencyId,
            string receiveCurrencyId,
            double sendAmount,/* double receiveAmount,*/
            int? productId,
            int? serviceCategoryId,
            int? instrumentId)
        {
            try
            {

                var merchantProducts = new List<MerchantProductComparisonDto>();
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DBConnection")))
                {
                    await connection.OpenAsync();
                    // Function call with query
                    var query = @"
                           SELECT * 
                            FROM public.getmerchantproductcomparisondata(
                            @p_sendamount,
                            @p_productid,
                            @p_instrumentid,
                            @p_servicecategoryid,
                            @p_receivecountry3iso,
                            @p_sendcountry3iso,
                            @p_receivecurrencyid,
                            @p_sendcurrencyid
                         )";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        //command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("p_sendamount", (object)(decimal)sendAmount ?? DBNull.Value);
                        command.Parameters.AddWithValue("p_productid", productId.HasValue ? (object)productId.Value : DBNull.Value);
                        command.Parameters.AddWithValue("p_instrumentid", instrumentId.HasValue ? (object)instrumentId.Value : DBNull.Value);
                        command.Parameters.AddWithValue("p_servicecategoryid", serviceCategoryId.HasValue ? (object)serviceCategoryId.Value : DBNull.Value);
                        command.Parameters.AddWithValue("p_receivecountry3iso", receiveCountry3Iso ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("p_sendcountry3iso", sendCountry3Iso ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("p_receivecurrencyid", receiveCurrencyId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("p_sendcurrencyid", sendCurrencyId ?? (object)DBNull.Value);


                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var merchantProduct = new MerchantProductComparisonDto
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    MerchantName = reader.GetString(reader.GetOrdinal("Merchant_Name")),
                                    AffiliateId = reader.GetString(reader.GetOrdinal("Affiliate_Id")),
                                    RoutingParameters = reader.GetString(reader.GetOrdinal("Routing_Parameters")),
                                    WebUrl = reader.GetString(reader.GetOrdinal("Web_Url")),
                                    MerchantType = reader.GetString(reader.GetOrdinal("Merchant_Type")),
                                    MerchantProductId = reader.GetInt32(reader.GetOrdinal("MerchantProductId")),
                                    ServiceCategoryId = reader.GetInt32(reader.GetOrdinal("ServiceCategory_Id")),
                                    InstrumentId = reader.GetInt32(reader.GetOrdinal("Instrument_Id")),
                                    ProductId = reader.GetInt32(reader.GetOrdinal("Product_Id")),
                                    SendCountry3Iso = reader.GetString(reader.GetOrdinal("Send_Country_3_iso")),
                                    ReceiveCountry3Iso = reader.GetString(reader.GetOrdinal("Receive_Country_3_iso")),
                                    SendCurrencyId = reader.GetString(reader.GetOrdinal("Send_Currency_Id")),
                                    ReceiveCurrencyId = reader.GetString(reader.GetOrdinal("Receive_Currency_Id")),
                                    ServiceLevels = reader.GetString(reader.GetOrdinal("Service_Levels")),
                                    FeesName = reader.GetString(reader.GetOrdinal("Fees_Name")),
                                    Fees = reader.GetDecimal(reader.GetOrdinal("Fees")),
                                    PromoFees = reader.GetDecimal(reader.GetOrdinal("Promo_Fees")),
                                    PayInInstrumentId = reader.GetInt32(reader.GetOrdinal("PayInInstrumentId")),
                                    VariableFee = reader.GetDecimal(reader.GetOrdinal("Variable_Fee")),
                                    TransferFee = reader.GetDecimal(reader.GetOrdinal("TransferFee")),
                                    FeeValidityExpiry = reader.GetFieldValue<DateTimeOffset>(reader.GetOrdinal("Fee_ValidityExpiry")),
                                    FeeSendMinLimit = reader.GetDecimal(reader.GetOrdinal("Fee_SendMinLimit")),
                                    FeeSendMaxLimit = reader.GetDecimal(reader.GetOrdinal("Fee_SendMaxLimit")),
                                    FeeReceiveMinLimit = reader.GetDecimal(reader.GetOrdinal("Fee_ReceiveMinLimit")),
                                    FeeReceiveMaxLimit = reader.GetDecimal(reader.GetOrdinal("Fee_ReceiveMaxLimit")),
                                    Rate = reader.GetDecimal(reader.GetOrdinal("Rate")),
                                    PromoRate = reader.GetDecimal(reader.GetOrdinal("Promo_Rate")),
                                    MerchantRateRef = reader.GetString(reader.GetOrdinal("Merchant_Rate_Ref")),
                                    RateValidityExpiry = reader.GetFieldValue<DateTimeOffset>(reader.GetOrdinal("Rate_ValidityExpiry")),
                                    RateSendMinLimit = reader.GetDecimal(reader.GetOrdinal("Rate_SendMinLimit")),
                                    RateSendMaxLimit = reader.GetDecimal(reader.GetOrdinal("Rate_SendMaxLimit")),
                                    RateReceiveMinLimit = reader.GetDecimal(reader.GetOrdinal("Rate_ReceiveMinLimit")),
                                    RateReceiveMaxLimit = reader.GetDecimal(reader.GetOrdinal("Rate_ReceiveMaxLimit")),
                                    TransferSpeed = ConvertServiceLevel(reader.GetString(reader.GetOrdinal("Service_Levels"))),
                                    MerchantTotalRate = reader.GetDecimal(reader.GetOrdinal("MerchantMarketRate")),
                                    MarketRate = reader.GetDecimal(reader.GetOrdinal("MarketRate")),
                                    RecipientGet = (Convert.ToDecimal(sendAmount) - reader.GetDecimal(reader.GetOrdinal("TransferFee"))) * reader.GetDecimal(reader.GetOrdinal("Rate")), // receii currenc
                                    InstrumentName = reader.GetString(reader.GetOrdinal("InstrumentName")),
                                    ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                    PayInInstrumentName = reader.GetString(reader.GetOrdinal("PayInInstrumentName")),
                                    ServiceCategoryName = reader.GetString(reader.GetOrdinal("ServiceCategoryName"))
                                };

                                merchantProducts.Add(merchantProduct);
                            }
                        }
                    }
                }
                var data = merchantProducts.OrderByDescending(x => x.RecipientGet).ToList();
                var bestPriceMerchant = data.FirstOrDefault().RecipientGet;
                //data = data.Select(x => bestPriceMerchant - x.RecipientGet / x.MarketRate).ToList();

                data.ForEach(x =>
                {
                    x.RecipientCommulativeFactor = Math.Round(((bestPriceMerchant - x.RecipientGet) / x.MarketRate), 3);
                    x.TotalCost = Math.Round(((Convert.ToDecimal(sendAmount) * x.MarketRate) - x.RecipientGet) / x.MarketRate, 3);
                });

                data = data.OrderBy(x => x.RecipientCommulativeFactor).ToList();

                //For FeaturedMerchant
                var activeAssets = await _activeAssets.GetAllActiveAssetRecord(_configuration.GetValue<int>("AssetMerchantFeaturedId"), null, true);

                if (activeAssets.Data != null)
                {
                    //for a country for now  only 1 featured merchant available
                    var activeAssetRecord = activeAssets.Data.ToList();
                    var merchantFeatured = data.DistinctBy(x => x.Id).ToList();
                    var featuredMerchants = (from merchantFeature in merchantFeatured
                                             join activeAsset in activeAssetRecord
                                             on merchantFeature.Id equals activeAsset.MerchantId
                                             select activeAsset.MerchantId).ToList();
                    data.ForEach(x =>
                    {
                        if (featuredMerchants.Contains(x.Id))
                        { 
                            x.FeaturedMerchant = true;
                        }
                    });

                }

                return data.OrderBy(x => x.FeaturedMerchant).ThenBy(x => x.RecipientCommulativeFactor).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
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

                throw new InvalidOperationException("An error occurred while calculating the markup " + ex.Message.ToString());
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

            if (string.IsNullOrEmpty(daysMatch.Value) && string.IsNullOrEmpty(hoursMatch.Value))
            {
                if (!string.IsNullOrEmpty(minutesMatch.Value))
                {
                    var replace = int.Parse(minutesMatch.Value.Replace("M", ""));
                    if (replace <= 5)
                    {
                        return "Instant";
                    }
                }
            }

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
