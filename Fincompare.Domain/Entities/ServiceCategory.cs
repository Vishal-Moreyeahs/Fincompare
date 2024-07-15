using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities
{
    public partial class ServiceCategory : DateBase
    {
        public int Id { get; set; }

        public string ServCategoryName { get; set; } = null!;

        public string Country3Iso { get; set; } = null!;

        public bool Status { get; set; }

        public virtual ICollection<ActiveAsset> ActiveAssets { get; set; } = new List<ActiveAsset>();

        public virtual Country Country3IsoNavigation { get; set; } = null!;

        public virtual ICollection<MerchantCampaign> MerchantCampaigns { get; set; } = new List<MerchantCampaign>();

        public virtual ICollection<MerchantProduct> MerchantProducts { get; set; } = new List<MerchantProduct>();

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }

}
