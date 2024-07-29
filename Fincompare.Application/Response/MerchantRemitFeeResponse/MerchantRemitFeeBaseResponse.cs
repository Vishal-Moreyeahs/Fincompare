namespace Fincompare.Application.Response.MerchantRemitFeeResponse
{
    public class MerchantRemitFeeBaseResponse
    {
        public class MerchantRemittanceFee
        {
            public int RemittanceFeeID { get; set; }
            public int MerchantID { get; set; }
            public string MerchantName { get; set; }
            public int ServiceCategoryId { get; set; }
            public string ServiceCategoryName { get; set; }
            public int InstrumentId { get; set; }
            public string InstrumentName { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string FeesName { get; set; }
            public string FeesCurrency { get; set; }
            public double Fees { get; set; }
            public double? PromoFees { get; set; }
            public int? MerchantProductID { get; set; }
            public string SendCountry { get; set; }
            public string ReceiveCountry { get; set; }
            public string SendCurrency { get; set; }
            public string ReceiveCurrency { get; set; }
            public double SendMinLimit { get; set; }
            public double SendMaxLimit { get; set; }
            public double ReceiveMinLimit { get; set; }
            public double ReceiveMaxLimit { get; set; }
            public DateTime ValidityExpiry { get; set; }
            public double? VariableFee { get; set; }
            public int? PayInInstrumentId { get; set; }
            public string? PayInInstrumentName { get; set; }
        }
    }

}
