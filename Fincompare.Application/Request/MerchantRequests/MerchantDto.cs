namespace Fincompare.Application.Request.MerchantRequests
{
    public class MerchantDto
    {
        public int Id { get; set; }

        public string MerchantName { get; set; } = null!;

        public string MerchantShortName { get; set; } = null!;

        public int GroupMerchantId { get; set; }

        public string MerchantCsph { get; set; } = null!;

        public string MerchantCsem { get; set; } = null!;

        public string Country3Iso { get; set; } = null!;

        public string? AffiliateId { get; set; }

        public string MerchantPh1 { get; set; } = null!;

        public string? MerchantPh2 { get; set; }

        public string MerchantEm1 { get; set; } = null!;

        public string? MerchantEm2 { get; set; }

        public string RoutingParameters { get; set; } = null!;

        public bool Status { get; set; }

        public string WebUrl { get; set; } = null!;

        public int UserId { get; set; }
    }
}
