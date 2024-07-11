namespace Fincompare.Domain.Entities
{
    public partial class CustomerReview
    {
        public int Id { get; set; }

        public int MerchantId { get; set; }

        public string Review { get; set; } = null!;

        public int Rating { get; set; }

        public bool Status { get; set; }

        public virtual Merchant Merchant { get; set; } = null!;
    }

}
