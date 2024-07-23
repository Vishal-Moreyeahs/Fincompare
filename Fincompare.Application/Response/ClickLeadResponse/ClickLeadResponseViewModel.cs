namespace Fincompare.Application.Response.ClickLeadResponse
{
    public class ClickLeadResponseViewModel
    {
        public int? CustomerUserId { get; set; }

        public string Country3Iso { get; set; } = null!;

        public int MerchantId { get; set; }
        public string MerchantName { get; set; }

        public string RoutingParamters { get; set; } = null!;

        public DateTime Date { get; set; }
    }
}
