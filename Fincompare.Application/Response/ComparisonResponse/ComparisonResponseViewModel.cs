using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Response.ComparisonResponse
{
    public class ComparisonResponseViewModel
    {
        public class MerchantProductComparisonDto
        {
            public int Id { get; set; }
            public string MerchantName { get; set; }
            public string AffiliateId { get; set; }
            public string RoutingParameters { get; set; }
            public string WebUrl { get; set; }
            public string MerchantType { get; set; }
            public int MerchantProductId { get; set; }
            public int ServiceCategoryId { get; set; }
            public string ServiceCategoryName { get; set; }

            public int InstrumentId { get; set; }
            public string InstrumentName { get; set; }

            public int ProductId { get; set; }
            public string ProductName { get; set; }

            public string SendCountry3Iso { get; set; }
            public string ReceiveCountry3Iso { get; set; }
            public string SendCurrencyId { get; set; }
            public string ReceiveCurrencyId { get; set; }
            public string ServiceLevels { get; set; }
            public string FeesName { get; set; }
            public decimal Fees { get; set; }
            public decimal PromoFees { get; set; }
            public int PayInInstrumentId { get; set; }
            public string PayInInstrumentName { get; set; }

            public decimal VariableFee { get; set; }
            public decimal TransferFee { get; set; }
            public string FeeStatus { get; set; }
            public DateTimeOffset FeeValidityExpiry { get; set; }
            public decimal FeeSendMinLimit { get; set; }
            public decimal FeeSendMaxLimit { get; set; }
            public decimal FeeReceiveMinLimit { get; set; }
            public decimal FeeReceiveMaxLimit { get; set; }
            public decimal Rate { get; set; }
            public decimal PromoRate { get; set; }
            public string MerchantRateRef { get; set; }
            public DateTimeOffset RateValidityExpiry { get; set; }
            public decimal RateSendMinLimit { get; set; }
            public decimal RateSendMaxLimit { get; set; }
            public decimal RateReceiveMinLimit { get; set; }
            public decimal RateReceiveMaxLimit { get; set; }
            public string TransferSpeed { get; set; }
            public decimal MerchantTotalRate { get; set; }
            public decimal MarketRate { get; set; }
            public decimal RecipientGet { get; set; }
            public decimal RecipientCommulativeFactor { get; set; }
            public decimal TotalCost { get; set; }
            public bool FeaturedMerchant { get; set; }

        }

    }
}
