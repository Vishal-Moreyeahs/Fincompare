using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities
{
    public partial class MerchantProduct : Base
    {
        public int Id { get; set; }

        public int ServiceCategoryId { get; set; }

        public int InstrumentId { get; set; }

        public int ProductId { get; set; }

        public int MerchantId { get; set; }

        public string? SendCountry3Iso { get; set; }

        public string? ReceiveCountry3Iso { get; set; }

        public int SendCurrencyId { get; set; }

        public int ReceiveCurrencyId { get; set; }

        public bool Status { get; set; }

        public string ServiceLevels { get; set; } = null!;

        public virtual Instrument Instrument { get; set; } = null!;

        public virtual Merchant Merchant { get; set; } = null!;

        public virtual ICollection<MerchantCampaign> MerchantCampaigns { get; set; } = new List<MerchantCampaign>();

        public virtual ICollection<MerchantProductCoupon> MerchantProductCoupons { get; set; } = new List<MerchantProductCoupon>();

        public virtual ICollection<MerchantRemitProductFee> MerchantRemitProductFees { get; set; } = new List<MerchantRemitProductFee>();

        public virtual ICollection<MerchantRemitProductRate> MerchantRemitProductRates { get; set; } = new List<MerchantRemitProductRate>();

        public virtual Product Product { get; set; } = null!;

        public virtual Country? ReceiveCountry3IsoNavigation { get; set; }

        public virtual Currency ReceiveCurrency { get; set; } = null!;

        public virtual Country? SendCountry3IsoNavigation { get; set; }

        public virtual Currency SendCurrency { get; set; } = null!;

        public virtual ServiceCategory ServiceCategory { get; set; } = null!;
    }

}
