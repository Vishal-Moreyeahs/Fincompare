using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Request.MarketRateRequest
{
    public class MarketRateRequests
    {
        public int SendCur { get; set; }

        public int ReceiveCur { get; set; }

        public double Rate { get; set; }

        public DateTime Date { get; set; }

        public string RateSource { get; set; } = null!;
    }

    public class AddMarketRate : MarketRateRequests { }

    public class UpdateMarketRate : MarketRateRequests { 
        public int Id { get; set; }
    }

}
