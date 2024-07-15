using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities
{
    public partial class Coupon : DateBase
    {
        public int Id { get; set; }

        public string CouponName { get; set; } = null!;

        public string? CouponFormat { get; set; }

        public bool Status { get; set; }

        public virtual ICollection<MerchantProductCoupon> MerchantProductCoupons { get; set; } = new List<MerchantProductCoupon>();
    }

}
