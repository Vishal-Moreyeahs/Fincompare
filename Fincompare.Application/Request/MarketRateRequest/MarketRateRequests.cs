namespace Fincompare.Application.Request.MarketRateRequest
{
    public class MarketRateRequests
    {
        public string SendCur { get; set; }

        public string ReceiveCur { get; set; }

        public double Rate { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string RateSource { get; set; } = "Capture";
    }

    public class AddMarketRate : MarketRateRequests { }

    public class UpdateMarketRate : MarketRateRequests
    {
        public int Id { get; set; }
    }

}
