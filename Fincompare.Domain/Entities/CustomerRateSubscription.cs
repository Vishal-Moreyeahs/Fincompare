namespace Fincompare.Domain.Entities
{
    public partial class CustomerRateSubscription
    {
        public int Id { get; set; }

        public int CustomerUserId { get; set; }

        public string SendCur { get; set; }

        public string ReceiveCur { get; set; }

        public double WishRate { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual CustomerUser CustomerUser { get; set; } = null!;

        public virtual Currency ReceiveCurNavigation { get; set; } = null!;

        public virtual Currency SendCurNavigation { get; set; } = null!;
    }

}
