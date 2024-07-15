namespace Fincompare.Application.Response.GroupMerchantResponse
{
    public class GroupMerchantViewResponse
    {
        public class GetAllGroupMerchantResponse
        {
            public int Id { get; set; }
            public string GroupMerchantName { get; set; } = null!;
            public string GroupMerchantShortName { get; set; } = null!;
            public string GroupPh1 { get; set; } = null!;
            public string? GroupPh2 { get; set; }
            public string GroupEm1 { get; set; } = null!;
            public string? GroupEm2 { get; set; }
            public string GroupCsph { get; set; } = null!;
            public string GroupCsem { get; set; } = null!;
            public string Country3Iso { get; set; } = null!;
            public bool Status { get; set; }
        }
    }
}
