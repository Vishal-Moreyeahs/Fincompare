namespace Fincompare.Application.Request.CustomerUsedCouponRequest
{
    public class CustomerUsedCouponViewRequest
    {
        public class CreateCustomerUsedCouponRequest
        {
            public int MerchantCouponId { get; set; }
            public string CouponCode { get; set; } = null!;
            public int CustomerId { get; set; }
            public bool IsUsed { get; set; }
            public DateTime UsedDateTime { get; set; }
        }
        
    }
}
