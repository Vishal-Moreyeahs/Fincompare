namespace Fincompare.Application.Request.CouponRequest
{
    public class CouponRequestViewModel
    {
        public class CreateCouponRequest
        {
            public string CouponName { get; set; } = null!;
            public string? CouponFormat { get; set; }
            public bool Status { get; set; }
        }

        public class UpdateCouponRequest : CreateCouponRequest
        {
            public int Id { get; set; }
        }
    }
}
