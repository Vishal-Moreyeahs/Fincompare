namespace Fincompare.Application.Response.CustomerUsedCouponResponse
{
    public class CustomerUsedCouponViewResponse
    {
        public class GetAllCustomerUsedCouponResponse
        {
            public int CustomerUsedCouponId { get; set; }
            public int MerchantCouponId { get; set; }
            public int Merchant { get; set; }
            public int CustomerId { get; set; }
            public string couponCode { get; set; }
            public bool IsUsed { get; set; }
            public DateTime UsedDateTime { get; set; }
        }

        public class CreateCustomerUsedResponse
        {
            public int MerchantCouponId { get; set; }
            public int CustomerId { get; set; }
            public string CouponCode { get; set; }
            public bool IsUsed { get; set; }
            public DateTime UsedDateTime { get; set; }
        }
    }
}
