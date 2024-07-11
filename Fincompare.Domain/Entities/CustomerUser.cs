﻿using Fincompare.Domain.Entities.UserManagementEntities;

namespace Fincompare.Domain.Entities
{
    public partial class CustomerUser
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = null!;

        public string EmailId { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string State { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Country3Iso { get; set; } = null!;

        public bool RateSubscription { get; set; }

        public bool PromoSubscription { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }

        public virtual ICollection<ClickLead> ClickLeads { get; set; } = new List<ClickLead>();

        public virtual Country Country3IsoNavigation { get; set; } = null!;

        public virtual ICollection<CustomerRateSubscription> CustomerRateSubscriptions { get; set; } = new List<CustomerRateSubscription>();

        public virtual ICollection<CustomerUsedCoupon> CustomerUsedCoupons { get; set; } = new List<CustomerUsedCoupon>();

        public virtual User User { get; set; } = null!;
    }

}
