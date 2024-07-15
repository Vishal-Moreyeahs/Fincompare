using Fincompare.Domain.Entities.Common;

namespace Fincompare.Domain.Entities
{
    public partial class ActiveAsset : ActionBase
    {
        public int Id { get; set; }

        public int AssetsMasterId { get; set; }

        public int AssetDescription { get; set; }

        public int MerchantId { get; set; }

        public string Country3Iso { get; set; } = null!;

        public string AssetMerchantUrl { get; set; } = null!;

        public int ServiceCategoryId { get; set; }

        public DateTime Date { get; set; }

        public bool Status { get; set; }

        public DateTime DateActive { get; set; }

        public DateTime DateValidity { get; set; }

        public virtual AssetsMaster AssetsMaster { get; set; } = null!;

        public virtual Country Country3IsoNavigation { get; set; } = null!;

        public virtual Merchant Merchant { get; set; } = null!;

        public virtual ServiceCategory ServiceCategory { get; set; } = null!;
    }

}
