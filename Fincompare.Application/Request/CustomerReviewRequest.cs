using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request
{
    public class CustomerReviewRequest
    {
        [Required]
        public int MerchantId { get; set; }

        [Required]
        public string Review { get; set; } = null!;

        [Required]
        public int Rating { get; set; }

        public bool Status { get; set; } = true;
    }

    public class AddCustomerReviewRequest : CustomerReviewRequest { }
    public class UpdateCustomerReviewRequest : CustomerReviewRequest
    {
        [Required]
        public int Id { get; set; }
    }

}
