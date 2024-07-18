using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.GroupMerchantRequest
{
    public class GroupMerchantBaseModel
    {
        public class AddGroupMerchantRequestClass
        {
            public string GroupMerchantName { get; set; } = null!;
            public string GroupMerchantShortName { get; set; } = null!;

            [Phone]
            public string GroupPh1 { get; set; } = null!;

            [Phone]
            public string? GroupPh2 { get; set; }

            [EmailAddress]
            public string GroupEm1 { get; set; } = null!;

            [EmailAddress]
            public string? GroupEm2 { get; set; }

            [Phone]
            public string GroupCsph { get; set; } = null!;

            [EmailAddress]
            public string GroupCsem { get; set; } = null!;
            public string Country3Iso { get; set; } = null!;
            public bool Status { get; set; }
        }
        public class UpdateGroupMerchantRequestClass : AddGroupMerchantRequestClass
        {
            public int Id { get; set; }
        }
    }
}
