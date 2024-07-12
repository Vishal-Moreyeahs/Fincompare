using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Request.MarketRateRequest
{
    public class MarketRateDto
    {
        public int Id { get; set; }

        public int SendCur { get; set; }

        public int ReceiveCur { get; set; }

        public double Rate { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string RateSource { get; set; }
    }
}
