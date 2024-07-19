namespace Fincompare.Application.Request.ServiceCategoriesRequest
{
    public class ServiceCategoriesViewModel
    {
        public class CreateServiceCategoriesRequest
        {

            public string ServiceCategoryName { get; set; } = null!;
            public string Country3Iso { get; set; } = null!;
            public bool Status { get; set; }
        }

        public class UpdateServiceCategoriesRequest : CreateServiceCategoriesRequest
        {
            public int Id { get; set; }
        }
    }
}
