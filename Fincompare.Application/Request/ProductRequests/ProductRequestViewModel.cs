namespace Fincompare.Application.Request.ProductRequests
{
    public class ProductRequestViewModel
    {
        public class CreateProductRequest
        {
            public string ProductName { get; set; } = null!;
            public string ProductDescription { get; set; } = null!;
            public int ServiceCategoryId { get; set; }
            public string Country3Iso { get; set; } = null!;
            public bool Status { get; set; }
        }
        public class UpdateProductRequest : CreateProductRequest
        {
            public int Id { get; set; }
        }
    }
}
