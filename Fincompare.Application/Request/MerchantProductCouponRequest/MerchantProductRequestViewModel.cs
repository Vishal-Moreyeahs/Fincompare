namespace Fincompare.Application.Request.MerchantProductCouponRequest
{
    public class MerchantProductRequestViewModel
    {
        public class CreateMerchantProductCouponRequest
        {
            public int CouponId { get; set; }
            public int? MerchantProductId { get; set; }
            public string CouponCode { get; set; } = null!;
            public bool IsMultiple { get; set; }
            public DateTime ValidityFrom { get; set; }
            public DateTime ValidityTo { get; set; }
            public bool Status { get; set; }
            public string? MerchantCouponBatch { get; set; } = null;

        }

        public class UpdateMerchantProductCouponRequest : CreateMerchantProductCouponRequest
        {
            public int MerchantCouponId { get; set; }
            public int MerchantId { get; set; }
        }
    }
}
