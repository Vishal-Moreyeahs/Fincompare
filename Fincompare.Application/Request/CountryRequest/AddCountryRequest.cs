using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.CountryRequest
{
    public class CountryRequest
    {
        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The Country3Iso field must be exactly 3 characters long.")]
        public string Country3Iso { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "The Country2Iso field must be exactly 3 characters long.")]

        public string Country2Iso { get; set; }

        [Required]
        public string CountryName { get; set; } = null!;
        public bool Status { get; set; } = true;
        public string? WebLink { get; set; }

    }
}
