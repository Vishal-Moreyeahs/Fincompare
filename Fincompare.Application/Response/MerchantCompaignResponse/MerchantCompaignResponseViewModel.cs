using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Response.MerchantCompaignResponse
{
    public class MerchantCompaignResponseViewModel
    {
        public int MerchantCampaignId { get; set; }
        public string CampaignCode { get; set; }
        public string CampaignDescription { get; set; }
        public int MerchantID { get; set; }
        public string MerchantName { get; set; }
        public int ServiceCategoryId { get; set; }
        public string ServiceCategoryName { get; set; }
        public int InstrumentId { get; set; }
        public string InstrumentName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? MerchantProductId { get; set; }
        public string SendCountry3Iso { get; set; }
        public string ReceiveCountry3Iso { get; set; }
        public DateTime DateActive { get; set; }
        public DateTime DateValidity { get; set; }
        public bool Status { get; set; }
    }
}
