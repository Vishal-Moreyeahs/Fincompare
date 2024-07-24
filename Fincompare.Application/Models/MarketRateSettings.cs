using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Models
{
    public class MarketRateSettings
    {
        public string MarketRateApiKey { get; set; }
        public int ScheduledTimePeriodInHours { get; set; }
    }

}
