using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.ServiceCategoriesRequest
{
    public class ServiceCategoriesViewModel
    {
        public class CreateServiceCategoriesRequest
        {
            [Required]
            public string ServiceCategoryName { get; set; } = null!;

            public bool Status { get; set; } = true;
        }

        public class UpdateServiceCategoriesRequest : CreateServiceCategoriesRequest
        {
            [Required]
            public int Id { get; set; }
        }
    }
}
