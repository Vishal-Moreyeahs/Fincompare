using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.MerchantRequests
{
    public class MerchantRequest
    {
        [Required]
        public string MerchantName { get; set; } = null!;

        public string MerchantShortName { get; set; } = null!;

        [Required]
        public int GroupMerchantId { get; set; }

        [Required]
        [Phone]
        public string MerchantCsph { get; set; } = null!;

        public string MerchantCsem { get; set; } = null!;

        public string Country3Iso { get; set; } = null!;

        public string? AffiliateId { get; set; }

        [Required]
        [Phone]
        public string MerchantPh1 { get; set; } = null!;

        [Phone]
        public string? MerchantPh2 { get; set; }

        [Required]
        [EmailAddress]
        public string MerchantEm1 { get; set; } = null!;

        [EmailAddress]
        public string? MerchantEm2 { get; set; }

        public string RoutingParameters { get; set; } = null!;

        public bool Status { get; set; } = true;

        public string WebUrl { get; set; } = null!;
        public string? MerchantType { get; set; }


    }

    public class AddMerchantRequest : MerchantRequest { }

    public class UpdateMerchantRequest : MerchantRequest
    {
        [Required]
        public int Id { get; set; }
        public int? UserId { get; set; }
    }
}
