using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.MerchantProductRequests
{
    public class MerchantProductRequest
    {
        [Required]
        public int ServiceCategoryId { get; set; }

        [Required]
        public int PayoutInstrumentId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int MerchantId { get; set; }


        [StringLength(3, MinimumLength = 3, ErrorMessage = "The SendCountry3Iso field must be exactly 3 characters long.")]
        public string? SendCountry3Iso { get; set; }

        [StringLength(3, MinimumLength = 3, ErrorMessage = "The ReceiveCountry3Iso field must be exactly 3 characters long.")]
        public string? ReceiveCountry3Iso { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The SendCurrency3Iso field must be exactly 3 characters long.")]
        public string SendCurrencyId { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The ReceiveCurrency3Iso field must be exactly 3 characters long.")]
        public string ReceiveCurrencyId { get; set; }

        public bool Status { get; set; } = true;

        [Required]
        [RegularExpression(@"^(?:(\d+D)?(\d+H)?(\d+M)?)$", ErrorMessage = "Invalid format for ServiceLevels.")]
        public string ServiceLevels { get; set; } = null!;
    }

    public class AddMerchantProductRequest : MerchantProductRequest { }
    public class UpdateMerchantProductRequest : MerchantProductRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
