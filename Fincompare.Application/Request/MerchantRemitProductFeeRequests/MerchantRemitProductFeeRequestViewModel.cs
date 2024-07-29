using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.MerchantRemitProductFeeRequests
{
    public class MerchantRemitProductFeeRequestViewModel
    {
        public class CreateMerchantRemitProductFeeRequest
        {
            [Required]
            public int MerchantId { get; set; }
            public string FeesName { get; set; } = null!;

            [Required]
            public string FeesCur { get; set; }

            [Required]
            public double Fees { get; set; }
            public double? PromoFees { get; set; }
            public int? MerchantProductId { get; set; }
            public string SendCountry3Iso { get; set; } = null!;
            public string ReceiveCountry3Iso { get; set; } = null!;
            public string SendCurrency { get; set; }
            public string ReceiveCurrency { get; set; }
            public double SendMinLimit { get; set; }
            public double SendMaxLimit { get; set; }
            public double ReceiveMinLimit { get; set; }
            public double ReceiveMaxLimit { get; set; }
            public DateTime ValidityExpiry { get; set; }
            public int? PayInInstrumentId { get; set; }
            public double VariableFee { get; set; }
        }
        public class UpdateMerchantRemitProductFeeRequest : CreateMerchantRemitProductFeeRequest
        {
            [Required]
            public int Id { get; set; }
        }
    }
}
