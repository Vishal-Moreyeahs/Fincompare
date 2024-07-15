using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities
{
    public partial class Country : DateBase
    {
        public string Country3Iso { get; set; } = null!;

        public string? Country2Iso { get; set; }

        public string CountryName { get; set; } = null!;

        public string? WebLink { get; set; }

        public bool Status { get; set; } = true;

        public virtual ICollection<ActiveAsset> ActiveAssets { get; set; } = new List<ActiveAsset>();

        public virtual ICollection<AssetsMaster> AssetsMasters { get; set; } = new List<AssetsMaster>();

        public virtual ICollection<ClickLead> ClickLeads { get; set; } = new List<ClickLead>();

        public virtual ICollection<CountryCurrency> CountryCurrencies { get; set; } = new List<CountryCurrency>();

        public virtual ICollection<CustomerUser> CustomerUsers { get; set; } = new List<CustomerUser>();

        public virtual ICollection<GroupMerchant> GroupMerchants { get; set; } = new List<GroupMerchant>();

        public virtual ICollection<Instrument> Instruments { get; set; } = new List<Instrument>();

        public virtual ICollection<MerchantCampaign> MerchantCampaignReceiveCountry3IsoNavigations { get; set; } = new List<MerchantCampaign>();

        public virtual ICollection<MerchantCampaign> MerchantCampaignSendCountry3IsoNavigations { get; set; } = new List<MerchantCampaign>();

        public virtual ICollection<MerchantProduct> MerchantProductReceiveCountry3IsoNavigations { get; set; } = new List<MerchantProduct>();

        public virtual ICollection<MerchantProduct> MerchantProductSendCountry3IsoNavigations { get; set; } = new List<MerchantProduct>();

        public virtual ICollection<MerchantRemitProductFee> MerchantRemitProductFeeReceiveCountry3IsoNavigations { get; set; } = new List<MerchantRemitProductFee>();

        public virtual ICollection<MerchantRemitProductFee> MerchantRemitProductFeeSendCountry3IsoNavigations { get; set; } = new List<MerchantRemitProductFee>();

        public virtual ICollection<MerchantRemitProductRate> MerchantRemitProductRateReceiveCountry3IsoNavigations { get; set; } = new List<MerchantRemitProductRate>();

        public virtual ICollection<MerchantRemitProductRate> MerchantRemitProductRateSendCountry3IsoNavigations { get; set; } = new List<MerchantRemitProductRate>();

        public virtual ICollection<Merchant> Merchants { get; set; } = new List<Merchant>();

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        public virtual ICollection<RateCard> RateCards { get; set; } = new List<RateCard>();

        public virtual ICollection<ServiceCategory> ServiceCategories { get; set; } = new List<ServiceCategory>();

        public virtual ICollection<State> States { get; set; } = new List<State>();
    }

}
