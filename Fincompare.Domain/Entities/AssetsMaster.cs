namespace Fincompare.Domain.Entities
{
    public partial class AssetsMaster
    {
        public int Id { get; set; }

        public string AssetName { get; set; } = null!;

        public string AssetDescription { get; set; } = null!;

        public string Country3Iso { get; set; } = null!;

        public DateTime Date { get; set; }

        public bool Status { get; set; }

        public virtual ICollection<ActiveAsset> ActiveAssets { get; set; } = new List<ActiveAsset>();

        public virtual Country Country3IsoNavigation { get; set; } = null!;
    }

}
