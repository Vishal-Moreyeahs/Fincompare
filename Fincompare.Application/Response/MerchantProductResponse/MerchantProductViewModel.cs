using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Response.MerchantProductResponse
{
    public class MerchantProductViewModel
    {
        public int MerchantProductId { get; set; }

        public int ServiceCategoryId { get; set; }
        public string ServiceCategoryName { get; set; }

        public int InstrumentId { get; set; }
        public string InstrumentName { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int MerchantId { get; set; }
        public string MerchantName { get; set; }

        public string? SendCountry3Iso { get; set; }

        public string? ReceiveCountry3Iso { get; set; }

        public string SendCurrencyId { get; set; }

        public string ReceiveCurrencyId { get; set; }

        public bool Status { get; set; }

        public string ServiceLevels { get; set; } = null!;
    }
}
