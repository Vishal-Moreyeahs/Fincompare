namespace Fincompare.Application.Request.MerchantRemitProductFeeRequests
{
    public class MerchantRemitProductFeeRequestViewModel
    {
        public class CreateMerchantRemitProductFeeRequest
        {
            public int MerchantId { get; set; }
            public string FeesName { get; set; } = null!;
            public string FeesCur { get; set; }
            public double Fees { get; set; }
            public double? PromoFees { get; set; }
            public int? MerchantProductId { get; set; }
            public string SendCountry3Iso { get; set; } = null!;
            public string ReceiveCountry3Iso { get; set; } = null!;
            public string SendCurrency { get; set; }
            public string ReceiveCurrency { get; set; }
            public double SendMinLimit { get; set; }
            public double SendMaxLimit { get; set; }
            public double ReceiveMinLimit { get; set; }
            public double ReceiveMaxLimit { get; set; }
            public DateTime ValidityExpiry { get; set; }
        }
        public class UpdateMerchantRemitProductFeeRequest : CreateMerchantRemitProductFeeRequest
        {
            public int RemittanceFeeID { get; set; }
        }
    }
}
