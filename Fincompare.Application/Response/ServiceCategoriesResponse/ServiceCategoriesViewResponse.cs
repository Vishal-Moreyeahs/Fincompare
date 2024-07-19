namespace Fincompare.Application.Response.ServiceCategoriesResponse
{
    public class ServiceCategoriesViewResponse
    {
        public class GetAllServiceCategoriesResponse
        {
            public int Id { get; set; } = 0;
            public string ServiceCategoryName { get; set; } = null!;
            public string Country3Iso { get; set; } = null!;
            public bool Status { get; set; }
        }
    }
}
