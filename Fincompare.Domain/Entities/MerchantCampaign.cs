using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities
{
    public partial class MerchantCampaign : ActionBase
    {
        public int Id { get; set; }

        public string CampaignCode { get; set; } = null!;

        public string CampaignDescription { get; set; } = null!;

        public int MerchantId { get; set; }

        public int ServiceCategoryId { get; set; }

        public int? MerchantProductId { get; set; }

        public string SendCountry3Iso { get; set; } = null!;

        public string ReceiveCountry3Iso { get; set; } = null!;

        public DateTime Date { get; set; }

        public bool Status { get; set; }

        public DateTime DateActive { get; set; }

        public DateTime DateValidity { get; set; }

        public virtual Merchant Merchant { get; set; } = null!;

        public virtual MerchantProduct? MerchantProduct { get; set; }

        public virtual Country ReceiveCountry3IsoNavigation { get; set; } = null!;

        public virtual Country SendCountry3IsoNavigation { get; set; } = null!;

        public virtual ServiceCategory ServiceCategory { get; set; } = null!;
    }

}
