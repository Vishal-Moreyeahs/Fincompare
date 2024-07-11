namespace Fincompare.Domain.Entities
{
    public partial class CustomerUsedCoupon
    {
        public int Id { get; set; }

        public int MerchantProductCouponId { get; set; }

        public string CouponCode { get; set; } = null!;

        public int? MerchantId { get; set; }

        public int CustomerUserId { get; set; }

        public bool IsUsed { get; set; }

        public DateTime UsedDateTime { get; set; }

        public virtual CustomerUser CustomerUser { get; set; } = null!;

        public virtual Merchant? Merchant { get; set; }

        public virtual MerchantProductCoupon MerchantProductCoupon { get; set; } = null!;
    }

}
