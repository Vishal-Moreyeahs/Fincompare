using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.ServiceCategoriesRequest
{
    public class ServiceCategoriesViewModel
    {
        public class CreateServiceCategoriesRequest
        {
            [Required]
            public string ServiceCategoryName { get; set; } = null!;

            [Required]
            [StringLength(3, MinimumLength = 3, ErrorMessage = "The Country3Iso field must be exactly 3 characters long.")]
            public string Country3Iso { get; set; } = null!;
            public bool Status { get; set; } = true;
        }

        public class UpdateServiceCategoriesRequest : CreateServiceCategoriesRequest
        {
            [Required]
            public int Id { get; set; }
        }
    }
}
