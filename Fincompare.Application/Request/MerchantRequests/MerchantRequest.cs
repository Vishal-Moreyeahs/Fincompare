using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.MerchantRequests
{
    public class MerchantRequest
    {
        [Required]
        public string MerchantName { get; set; } = null!;

        [Required]
        [MinLength(2, ErrorMessage = "The MerchantShortName field minimnum 2 characters long.")]
        public string MerchantShortName { get; set; } = null!;

        [Required]
        public int GroupMerchantId { get; set; }

        [Required]
        [RegularExpression(@"^(\+[0-9]{2}|\+[0-9]{2}\(0\)|\(\+[0-9]{2}\)\(0\)|00[0-9]{2}|0)?([0-9]{9}|[0-9\-]{9,18})$", ErrorMessage = "Not a valid phone number")]
        [DefaultValue("1234567890")]
        public string MerchantCsph { get; set; } = null!;

        [Required]
        [MinLength(3)]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$",
        ErrorMessage = "Email is required and must be properly formatted.")]
        public string MerchantCsem { get; set; } = null!;

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The CountryIso3 field must be exactly 3 characters long.")]
        public string Country3Iso { get; set; } = null!;

        [MinLength(3, ErrorMessage = "The AffiliateId field minimum 3 characters long.")]
        public string? AffiliateId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "The Phone field minimnum 3 characters long.")]
        [RegularExpression(@"^(\+[0-9]{2}|\+[0-9]{2}\(0\)|\(\+[0-9]{2}\)\(0\)|00[0-9]{2}|0)?([0-9]{9}|[0-9\-]{9,18})$", ErrorMessage = "Not a valid phone number")]
        public string MerchantPh1 { get; set; } = null!;

        [RegularExpression(@"^(\+[0-9]{2}|\+[0-9]{2}\(0\)|\(\+[0-9]{2}\)\(0\)|00[0-9]{2}|0)?([0-9]{9}|[0-9\-]{9,18})$", ErrorMessage = "Not a valid phone number")]
        [MinLength(3, ErrorMessage = "The Phone field minimnum 3 characters long.")]
        public string? MerchantPh2 { get; set; }

        [Required]
        [MinLength(3)]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$",
        ErrorMessage = "Email is required and must be properly formatted.")]
        public string MerchantEm1 { get; set; } = null!;

        [MinLength(3)]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$",
        ErrorMessage = "Email is required and must be properly formatted.")]
        public string? MerchantEm2 { get; set; }

        [Required]
        public string RoutingParameters { get; set; } = null!;

        public bool Status { get; set; } = true;

        [Required]
        public string WebUrl { get; set; } = null!;

        private string? _merchantType;

        [Required]
        [RegularExpression("^(?i)(Instore|Online)$", ErrorMessage = "MerchantType must be either 'Instore' or 'Online'.")]
        [DisplayName("Instore/Online")]
        public string? MerchantType
        {
            get => _merchantType;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _merchantType = value.ToLower() switch
                    {
                        "instore" => "Instore",
                        "online" => "Online",
                        _ => value
                    };
                }
            }
        }

        [Required]
        public bool IsPartneredMerchant { get; set; }

    }

    public enum MerchantType
    {
        Instore,
        Online
    }

    public class AddMerchantRequest : MerchantRequest { }

    public class UpdateMerchantRequest : AddMerchantRequest
    {
        [Required]
        public int Id { get; set; }
        public int? UserId { get; set; }
    }
}
