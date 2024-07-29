using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Response.ActiveAssetResponse
{
    public class ActiveAssetResponseViewModel
    {
        public int Id { get; set; }

        public int AssetsMasterId { get; set; }

        public int AssetDescription { get; set; }

        public int MerchantId { get; set; }

        public string Country3Iso { get; set; } = null!;

        public string AssetMerchantUrl { get; set; } = null!;

        public int ServiceCategoryId { get; set; }

        public DateTime Date { get; set; }

        public bool Status { get; set; }

        public DateTime DateActive { get; set; }

        public DateTime DateValidity { get; set; }
        public int? MerchantAssetPriority { get; set; }

    }
}
