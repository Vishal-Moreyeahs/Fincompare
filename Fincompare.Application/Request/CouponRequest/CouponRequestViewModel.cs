using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.CouponRequest
{
    public class CouponRequestViewModel
    {
        public class CreateCouponRequest
        {
            [MinLength(3)]
            [MaxLength(25)]
            public string CouponName { get; set; } = null!;

            [MinLength(3)]
            [RegularExpression(@"^(Fee|CashBak)_(\d)(_%|_Fix|_First_Free)_(Off|Credit)$", ErrorMessage = "Invalid Coupon Format.")]
            public string? CouponFormat { get; set; }

            public bool Status { get; set; } = true;
        }

        public class UpdateCouponRequest : CreateCouponRequest
        {
            [Required]
            public int Id { get; set; }
        }
    }
}
