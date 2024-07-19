namespace Fincompare.Application.Response.MerchantProductCouponResponse
{
    public class MerchantProductCouponViewResponse
    {
        public class GetAllMerchantProductCouponResponse
        {
            public int MerchantCouponId { get; set; }
            public int CouponId { get; set; }
            public int? MerchantProductId { get; set; }
            public string CouponCode { get; set; } = null!;
            public bool IsMultiple { get; set; }
            public bool IsUsed { get; set; }
            public DateTime ValidityFrom { get; set; }
            public DateTime ValidityTo { get; set; }
            public bool Status { get; set; }
        }
        public class MerchantCouponResponseClass
        {
            public int MerchantCouponId { get; set; }
            public int CouponId { get; set; }
            public int MerchantProductId { get; set; }
            public int MerchantId { get; set; }
            public string MerchantName { get; set; }
            public int ServiceCategoryId { get; set; }
            public string ServiceCategoryName { get; set; }
            public int InstrumentId { get; set; }
            public string InstrumentName { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string MerchantCouponBatch { get; set; }
            public string CouponCode { get; set; }
            public bool IsMultiple { get; set; }
            public bool IsUsed { get; set; }
            public string SendCountry { get; set; }
            public string ReceiveCountry { get; set; }
            public string SendCurrency { get; set; }
            public string ReceiveCurrency { get; set; }
            public DateTime ValidityFrom { get; set; }
            public DateTime ValidityTo { get; set; }
            public bool Status { get; set; }
        }
    }
}
