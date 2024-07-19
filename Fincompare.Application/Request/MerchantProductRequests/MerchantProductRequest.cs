using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.MerchantProductRequests
{
    public class MerchantProductRequest
    {
        [Required]
        public int ServiceCategoryId { get; set; }

        [Required]
        public int InstrumentId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int MerchantId { get; set; }


        public string? SendCountry3Iso { get; set; }

        public string? ReceiveCountry3Iso { get; set; }

        [Required]
        public string SendCurrencyId { get; set; }

        [Required]
        public string ReceiveCurrencyId { get; set; }

        public bool Status { get; set; } = true;

        public string ServiceLevels { get; set; } = null!;
    }

    public class AddMerchantProductRequest : MerchantProductRequest { }
    public class UpdateMerchantProductRequest : MerchantProductDto { }
}
