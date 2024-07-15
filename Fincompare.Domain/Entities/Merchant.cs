using Fincompare.Domain.Entities.Common;
using Fincompare.Domain.Entities.UserManagementEntities;

namespace Fincompare.Domain.Entities
{
    public partial class Merchant : DateBase
    {
        public int Id { get; set; }

        public string MerchantName { get; set; } = null!;

        public string MerchantShortName { get; set; } = null!;

        public int GroupMerchantId { get; set; }

        public string MerchantCsph { get; set; } = null!;

        public string MerchantCsem { get; set; } = null!;

        public string Country3Iso { get; set; } = null!;

        public string? AffiliateId { get; set; }

        public string MerchantPh1 { get; set; } = null!;

        public string? MerchantPh2 { get; set; }

        public string MerchantEm1 { get; set; } = null!;

        public string? MerchantEm2 { get; set; }

        public string RoutingParameters { get; set; } = null!;

        public bool Status { get; set; }

        public string WebUrl { get; set; } = null!;

        public int UserId { get; set; }

        public virtual ICollection<ActiveAsset> ActiveAssets { get; set; } = new List<ActiveAsset>();

        public virtual ICollection<ClickLead> ClickLeads { get; set; } = new List<ClickLead>();

        public virtual Country Country3IsoNavigation { get; set; } = null!;

        public virtual ICollection<CustomerReview> CustomerReviews { get; set; } = new List<CustomerReview>();

        public virtual ICollection<CustomerUsedCoupon> CustomerUsedCoupons { get; set; } = new List<CustomerUsedCoupon>();

        public virtual GroupMerchant GroupMerchant { get; set; } = null!;

        public virtual ICollection<MerchantCampaign> MerchantCampaigns { get; set; } = new List<MerchantCampaign>();

        public virtual ICollection<MerchantProduct> MerchantProducts { get; set; } = new List<MerchantProduct>();

        public virtual ICollection<MerchantRemitProductFee> MerchantRemitProductFees { get; set; } = new List<MerchantRemitProductFee>();

        public virtual ICollection<MerchantRemitProductRate> MerchantRemitProductRates { get; set; } = new List<MerchantRemitProductRate>();

        public virtual User User { get; set; } = null!;
    }

}
