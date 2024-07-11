﻿using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities
{
    public partial class MerchantProductCoupon : Base
    {
        public int Id { get; set; }

        public int CouponId { get; set; }

        public int? MerchantProductId { get; set; }

        public string CouponCode { get; set; } = null!;

        public bool IsMultiple { get; set; }

        public bool IsUsed { get; set; }

        public DateTime ValidityFrom { get; set; }

        public DateTime ValidityTo { get; set; }

        public bool Status { get; set; }

        public virtual Coupon Coupon { get; set; } = null!;

        public virtual ICollection<CustomerUsedCoupon> CustomerUsedCoupons { get; set; } = new List<CustomerUsedCoupon>();

        public virtual MerchantProduct? MerchantProduct { get; set; }
    }

}
