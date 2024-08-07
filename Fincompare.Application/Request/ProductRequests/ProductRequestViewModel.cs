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

            public bool Status { get; set; }
        }
        public class UpdateProductRequest : CreateProductRequest
        {
            [Required]
            public int Id { get; set; }
        }
    }
}
