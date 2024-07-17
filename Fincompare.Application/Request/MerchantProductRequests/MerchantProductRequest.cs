using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Request.MerchantProductRequests
{
    public class MerchantProductRequest
    {

        public int ServiceCategoryId { get; set; }

        public int InstrumentId { get; set; }

        public int ProductId { get; set; }

        public int MerchantId { get; set; }

        public string? SendCountry3Iso { get; set; }

        public string? ReceiveCountry3Iso { get; set; }

        public string SendCurrencyId { get; set; }

        public string ReceiveCurrencyId { get; set; }

        public bool Status { get; set; }

        public string ServiceLevels { get; set; } = null!;
    }

    public class AddMerchantProductRequest : MerchantProductRequest { }
    public class UpdateMerchantProductRequest : MerchantProductDto { }
}
