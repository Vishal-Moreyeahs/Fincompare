using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.CountryRequest
{
    public class CountryRequest
    {
        [Required]
        public string Country3Iso { get; set; }

        [Required]
        public string Country2Iso { get; set; }

        [Required]
        public string CountryName { get; set; } = null!;
        public bool Status { get; set; } = true;
        public string? WebLink { get; set; }

    }
}
