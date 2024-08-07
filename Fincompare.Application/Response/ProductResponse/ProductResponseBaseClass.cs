namespace Fincompare.Application.Response.ProductResponse
{
    public class ProductResponseBaseClass
    {
        public class GetAllProductResponse
        {
            public int Id { get; set; }
            public string ProductName { get; set; } = null!;
            public string ProductDescription { get; set; } = null!;
            public int ServiceCategoryId { get; set; }
            public bool Status { get; set; }
        }
    }
}
