namespace Fincompare.Application.Response.CouponResponse
{
    public class CouponResponseViewModel
    {
        public class FetchCouponResponse
        {
            public int Id { get; set; }
            public string CouponName { get; set; } = null!;
            public string? CouponFormat { get; set; }
            public bool Status { get; set; }
        }
    }
}
