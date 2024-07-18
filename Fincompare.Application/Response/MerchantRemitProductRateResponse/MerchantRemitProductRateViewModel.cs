using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Response.MerchantRemitProductRateResponse
{
    public class MerchantRemitProductRateViewModel
    {
        public int Id { get; set; }

        public string MerchantRateRef { get; set; } = null!;

        public int MerchantId { get; set; }

        public string MerchantName { get; set; }

        public int? MerchantProductId { get; set; }

        public string ProductName { get; set; }
        public string InstrumentName { get; set; }
        public string ServiceCategoryName { get; set; }

        public string SendCountry3Iso { get; set; } = null!;

        public string ReceiveCountry3Iso { get; set; } = null!;

        public string SendCur { get; set; }

        public string ReceiveCur { get; set; }

        public int SendMinLimit { get; set; }

        public int SendMaxLimit { get; set; }

        public int ReceiveMinLimit { get; set; }

        public int ReceiveMaxLimit { get; set; }

        public double Rate { get; set; }

        public double PromoRate { get; set; }

        public DateTime ValidityExpiry { get; set; }

        public bool Status { get; set; }
    }
}
