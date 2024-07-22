using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Request.MerchantCompaignRequests
{
    public class MerchantCompaignRequest
    {
        [Required]
        public string CampaignCode { get; set; } = null!;

        [Required]
        public string CampaignDescription { get; set; } = null!;

        [Required]
        public int MerchantId { get; set; }

        [Required]
        public int ServiceCategoryId { get; set; }

        
        public int? MerchantProductId { get; set; }

        [Required]
        public string SendCountry3Iso { get; set; } = null!;

        [Required]
        public string ReceiveCountry3Iso { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        public bool Status { get; set; } = true;

        [Required]
        public DateTime DateActive { get; set; }

        [Required]
        public DateTime DateValidity { get; set; }
    }

    public class AddMerchantCompaignRequest : MerchantCompaignRequest{ }
    public class UpdateMerchantCompaignRequest : MerchantCompaignRequest{
        [Required]
        public int Id { get; set; }
    }
}
