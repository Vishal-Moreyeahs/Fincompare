using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.ProductRequests
{
    public class ProductRequestViewModel
    {
        public class CreateProductRequest
        {
            [Required]
            public string ProductName { get; set; } = null!;

            [Required]
            public string ProductDescription { get; set; } = null!;

            [Required]
            public int ServiceCategoryId { get; set; }

            [Required]
            [StringLength(3, MinimumLength = 3, ErrorMessage = "The CountryIso3 field must be exactly 3 characters long.")]
            public string Country3Iso { get; set; } = null!;
            public bool Status { get; set; }
        }
        public class UpdateProductRequest : CreateProductRequest
        {
            [Required]
            public int Id { get; set; }
        }
    }
}
