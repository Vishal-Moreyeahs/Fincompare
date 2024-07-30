using System.ComponentModel;
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
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\+[0-9]{2}|\+[0-9]{2}\(0\)|\(\+[0-9]{2}\)\(0\)|00[0-9]{2}|0)?([0-9]{9}|[0-9\-]{9,18})$", ErrorMessage = "Not a valid phone number")]
        [DefaultValue("1234567890")]
        public string MerchantCsph { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$",
        ErrorMessage = "Email is required and must be properly formatted.")]
        public string MerchantCsem { get; set; } = null!;

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The Country3Iso field must be exactly 3 characters long.")]
        public string Country3Iso { get; set; } = null!;

        public string? AffiliateId { get; set; }

        [Required]
        [Phone]
        public string MerchantPh1 { get; set; } = null!;

        [Phone]
        public string? MerchantPh2 { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$",
        ErrorMessage = "Email is required and must be properly formatted.")]
        public string MerchantEm1 { get; set; } = null!;

        [EmailAddress]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$",
        ErrorMessage = "Email is required and must be properly formatted.")]
        public string? MerchantEm2 { get; set; }

        public string RoutingParameters { get; set; } = null!;

        public bool Status { get; set; } = true;

        [Required]
        public string WebUrl { get; set; } = null!;

        [Required]
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
