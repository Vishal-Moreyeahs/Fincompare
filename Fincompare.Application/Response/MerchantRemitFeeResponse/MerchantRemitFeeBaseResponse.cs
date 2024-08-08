using Fincompare.Domain.Entities.UserManagementEntities;

namespace Fincompare.Application.Response.MerchantRemitFeeResponse
{
    public class MerchantRemitFeeBaseResponse
    {
        public class MerchantRemittanceFee
        {
            public int Id { get; set; }
            public int MerchantId { get; set; }
            public string MerchantName { get; set; }
            public int ServiceCategoryId { get; set; }
            public string ServiceCategoryName { get; set; }
            public int PayoutInstrumentId { get; set; }
            public string PayoutInstrumentName { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string FeesName { get; set; }
            public string FeesCurrency { get; set; }
            public double Fees { get; set; }
            public double? PromoFees { get; set; }
            public int? MerchantProductId { get; set; }
            public string SendCountry3Iso { get; set; }
            public string ReceiveCountry3Iso { get; set; }
            public string SendCurrency { get; set; }
            public string ReceiveCurrency { get; set; }
            public double SendMinLimit { get; set; }
            public double SendMaxLimit { get; set; }
            public double ReceiveMinLimit { get; set; }
            public double ReceiveMaxLimit { get; set; }
            public DateTime ValidityExpiry { get; set; }
            public decimal? VariableFee { get; set; }
            public int? PayInInstrumentId { get; set; }
            public string? PayInInstrumentName { get; set; }
            public bool Status { get; set; }
        }
    }

}
