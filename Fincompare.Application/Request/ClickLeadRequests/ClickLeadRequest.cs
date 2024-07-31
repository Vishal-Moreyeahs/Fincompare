using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.ClickLeadRequests
{
    public class ClickLeadRequest
    {
        public int? CustomerUserId { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The CountryIso3 field must be exactly 3 characters long.")]
        public string Country3Iso { get; set; } = null!;

        [Required]
        public int MerchantId { get; set; }

        [Required]
        public string RoutingParamters { get; set; } = null!;

        public DateTime Date { get; set; } = DateTime.UtcNow;
    }

    public class AddClickLeadRequest : ClickLeadRequest { }
    public class UpdateClickLeadRequest : ClickLeadRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
