using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.ActiveAssetRequests
{
    public class ActiveAssetRequest
    {

        [Required]
        public int AssetsMasterId { get; set; }

        [Required]
        public string AssetDescription { get; set; }

        [Required]
        public int MerchantId { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The CountryIso3 field must be exactly 3 characters long.")]
        public string Country3Iso { get; set; }

        [Required]
        public string AssetMerchantUrl { get; set; } = null!;

        [Required]
        public int ServiceCategoryId { get; set; }

        public DateTime Date { get; set; }

        public bool Status { get; set; } = true;

        [Required]
        public DateTime DateActive { get; set; }

        [Required]
        public DateTime DateValidity { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "The value must be between 1 and 10.")]
        public int? MerchantAssetPriority { get; set; }

    }

    public class AddActiveAssetRequest : ActiveAssetRequest { }

    public class UpdateActiveAssetRequest : ActiveAssetRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
