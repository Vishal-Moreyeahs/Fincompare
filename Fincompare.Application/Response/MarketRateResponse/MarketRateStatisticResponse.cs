namespace Fincompare.Application.Response.MarketRateResponse
{
    public class MarketRateStatisticsData
    {
        public List<RateDataModel> RateWeek { get; set; }
        public List<RateDataModel> Rates2Weeks { get; set; }
        public List<RateDataModel> RatesMonths { get; set; }

    }

    public class RateDataModel
    {
        public double Rate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
