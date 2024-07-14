namespace Fincompare.Domain.Entities
{
    public partial class MarketRate
    {
        public int Id { get; set; }

        public string SendCur { get; set; }

        public string ReceiveCur { get; set; }

        public double Rate { get; set; }

        public DateTime Date { get; set; }

        public string RateSource { get; set; } = null!;

        public virtual Currency ReceiveCurNavigation { get; set; } = null!;

        public virtual Currency SendCurNavigation { get; set; } = null!;
    }

}
