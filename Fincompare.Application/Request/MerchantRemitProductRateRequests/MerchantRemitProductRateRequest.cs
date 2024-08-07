using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.MerchantRemitProductRateRequests
{
    public class MerchantRemitProductRateRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string MerchantRateRef { get; set; } = null!;

        [Required]
        public int MerchantId { get; set; }

        //public int? MerchantProductId { get; set; }
        [Required]
        public int ServiceCategoryId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int InstrumentId { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The SendCountry3Iso field must be exactly 3 characters long.")]
        public string SendCountry3Iso { get; set; } = null!;

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The ReceiveCountry3Iso field must be exactly 3 characters long.")]
        public string ReceiveCountry3Iso { get; set; } = null!;

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The SendCurrency3Iso field must be exactly 3 characters long.")]
        public string SendCur { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The ReceiveCurrency3Iso field must be exactly 3 characters long.")]
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


        public double? PromoRate { get; set; } = 0;

        [Required]
        public DateTime ValidityExpiry { get; set; }

        public bool Status { get; set; } = true;
    }

    public class AddMerchantRemitProductRateRequest : MerchantRemitProductRateRequest { }
    public class UpdateMerchantRemitProductRateRequest : MerchantRemitProductRateRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
