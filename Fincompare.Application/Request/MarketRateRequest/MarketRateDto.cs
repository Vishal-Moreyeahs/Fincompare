namespace Fincompare.Application.Request.MarketRateRequest
{
    public class MarketRateDto
    {

        public string SendCur { get; set; }

        public string ReceiveCur { get; set; }

        public double Rate { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string RateSource { get; set; }
    }
}
