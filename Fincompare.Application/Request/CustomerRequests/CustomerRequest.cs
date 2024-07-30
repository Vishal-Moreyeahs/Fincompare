using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.CustomerRequests
{
    public class CustomerRequest
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailId { get; set; }


        public string Address { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }

        [StringLength(3, MinimumLength = 3, ErrorMessage = "The Country3Iso field must be exactly 3 characters long.")]
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
