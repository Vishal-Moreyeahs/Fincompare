using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.GroupMerchantRequest
{
    public class GroupMerchantBaseModel
    {
        public class AddGroupMerchantRequestClass
        {
            [Required]
            [MinLength(5)]
            [RegularExpression("^[a-zA-Z][a-zA-Z0-9]*$",
        ErrorMessage = "GroupMerchantName must have alpha numeric string.")]
            [MaxLength(55)]
            public string GroupMerchantName { get; set; } = null!;

            [Required]
            [MinLength(3)]
            [MaxLength(25)]
            public string GroupMerchantShortName { get; set; } = null!;


            [Required]
            [MinLength(3)]
            [DataType(DataType.PhoneNumber)]
            [RegularExpression(@"^(\+[0-9]{2}|\+[0-9]{2}\(0\)|\(\+[0-9]{2}\)\(0\)|00[0-9]{2}|0)?([0-9]{9}|[0-9\-]{9,18})$", ErrorMessage = "Not a valid phone number")]
            [DefaultValue("1234567890")]
            public string GroupPh1 { get; set; } = null!;


            [DataType(DataType.PhoneNumber)]
            [RegularExpression(@"^(\+[0-9]{2}|\+[0-9]{2}\(0\)|\(\+[0-9]{2}\)\(0\)|00[0-9]{2}|0)?([0-9]{9}|[0-9\-]{9,18})$", ErrorMessage = "Not a valid phone number")]
            [DefaultValue("1234567890")]
            public string? GroupPh2 { get; set; }

            [EmailAddress]
            [Required]
            [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$",
        ErrorMessage = "Email is required and must be properly formatted.")]
            public string GroupEm1 { get; set; } = null!;

            [EmailAddress]
            [MinLength(3)]
            [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$",
        ErrorMessage = "Email is required and must be properly formatted.")]
            public string? GroupEm2 { get; set; }

            [Required]
            [MinLength(3)]
            [DataType(DataType.PhoneNumber)]
            [RegularExpression(@"^(\+[0-9]{2}|\+[0-9]{2}\(0\)|\(\+[0-9]{2}\)\(0\)|00[0-9]{2}|0)?([0-9]{9}|[0-9\-]{9,18})$", ErrorMessage = "Not a valid phone number")]
            [DefaultValue("1234567890")]
            public string GroupCsph { get; set; } = null!;

            [Required]
            [EmailAddress]
            [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$",
        ErrorMessage = "Email is required and must be properly formatted.")]
            public string GroupCsem { get; set; } = null!;


            [Required]
            [StringLength(3, MinimumLength = 3, ErrorMessage = "The CountryIso3 field must be exactly 3 characters long.")]
            public string Country3Iso { get; set; } = null!;
            public bool Status { get; set; } = true;
        }
        public class UpdateGroupMerchantRequestClass : AddGroupMerchantRequestClass
        {
            [Required]
            public int Id { get; set; }
        }
    }
}
