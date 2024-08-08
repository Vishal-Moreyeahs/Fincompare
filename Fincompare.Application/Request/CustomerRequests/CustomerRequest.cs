using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.CustomerRequests
{
    public class CustomerRequest
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$",
        ErrorMessage = "Email is required and must be properly formatted.")]
        public string EmailId { get; set; }


        public string Address { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^(\+[0-9]{2}|\+[0-9]{2}\(0\)|\(\+[0-9]{2}\)\(0\)|00[0-9]{2}|0)?([0-9]{9}|[0-9\-]{9,18})$", ErrorMessage = "Not a valid phone number")]
        [DefaultValue("1234567890")]
        public string Phone { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The CountryIso3 field must be exactly 3 characters long.")]
        public string Country3Iso { get; set; }

        public string Password { get; set; } = string.Empty;
        public string RateSubscription { get; set; } = string.Empty;
        public string PromoSubscription { get; set; } = string.Empty;
        public string AuthProvider { get; set; } = string.Empty;
        public string AuthProviderId { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
    }

    public class AddCustomerRequest : CustomerRequest { }

    public class UpdateCustomerRequest
    {
        [Required]
        public int Id { get; set; }

        public string? CustomerName { get; set; }


        public string? EmailId { get; set; }


        public string? Address { get; set; } = string.Empty;

        public string? Phone { get; set; }
        public bool? Status { get; set; }
    }
}
