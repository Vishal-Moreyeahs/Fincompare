namespace Fincompare.Domain.Entities
{
    public partial class ClickLead
    {
        public int Id { get; set; }

        public int? CustomerUserId { get; set; }

        public string Country3Iso { get; set; } = null!;

        public int MerchantId { get; set; }

        public string RoutingParamters { get; set; } = null!;

        public DateTime Date { get; set; }

        public virtual Country Country3IsoNavigation { get; set; } = null!;

        public virtual CustomerUser? CustomerUser { get; set; }

        public virtual Merchant Merchant { get; set; } = null!;
    }

}
