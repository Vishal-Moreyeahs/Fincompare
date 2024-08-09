using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request
{
    public class CustomerReviewRequest
    {
        [Required]
        public int MerchantId { get; set; }

        [Required]
        [MinLength(3)]
        public string Review { get; set; } = null!;

        [Required]
        [Range(0, 9)]
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
