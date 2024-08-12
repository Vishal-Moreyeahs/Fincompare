using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.MerchantProductCouponRequest
{
    public class MerchantProductRequestViewModel
    {
        public class CreateMerchantProductCouponRequest
        {
            [Required]
            public int CouponId { get; set; }
            public int? MerchantProductId { get; set; }
            [Required]
            public string CouponCode { get; set; } = null!;
            [Required]
            public bool IsMultiple { get; set; } = false;
            public DateTime ValidityFrom { get; set; }
            public DateTime ValidityTo { get; set; }
            public bool Status { get; set; } = false;
            public string? MerchantCouponBatch { get; set; } = null;

        }

        public class UpdateMerchantProductCouponRequest : CreateMerchantProductCouponRequest
        {
            public int MerchantCouponId { get; set; }
            public int MerchantId { get; set; }
        }
    }
}
