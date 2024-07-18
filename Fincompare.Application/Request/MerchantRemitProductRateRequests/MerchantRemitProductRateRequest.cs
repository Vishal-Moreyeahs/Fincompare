using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Request.MerchantRemitProductRateRequests
{
    public class MerchantRemitProductRateRequest
    {
        [Required]
        public string MerchantRateRef { get; set; } = null!;

        [Required]
        public int MerchantId { get; set; }

        public int? MerchantProductId { get; set; }

        [Required]
        public string SendCountry3Iso { get; set; } = null!;

        [Required]
        public string ReceiveCountry3Iso { get; set; } = null!;

        [Required]
        public string SendCur { get; set; }

        [Required]
        public string ReceiveCur { get; set; }

        [Required]
        public int SendMinLimit { get; set; }

        [Required]
        public int SendMaxLimit { get; set; }

        [Required]
        public int ReceiveMinLimit { get; set; }

        [Required]
        public int ReceiveMaxLimit { get; set; }

        [Required]
        public double Rate { get; set; }

        [Required]
        public double PromoRate { get; set; }

        [Required]
        public DateTime ValidityExpiry { get; set; }

        public bool Status { get; set; } = true;
    }

    public class AddMerchantRemitProductRateRequest : MerchantRemitProductRateRequest { }
    public class UpdateMerchantRemitProductRateRequest : MerchantRemitProductRateRequest {
        [Required]
        public int Id { get; set; }
    }
}
