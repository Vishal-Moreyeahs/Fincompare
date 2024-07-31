using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.CountryCurrencyRequests
{
    public class CountryCurrencyRequest
    {
        [Required]
        public string Country3Iso { get; set; } = null!;

    }


    public class MultipleCurrencyRequest
    {
        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The CurrencyIso field must be exactly 3 characters long.")]
        public string CurrencyIso { get; set; }
        public bool IsPrimary { get; set; } = false;

        [Required]
        public string? Category { get; set; }
        public bool Status { get; set; } = true;

    }

    public class UpdateCountryWithMultipleCurrencyRequest : CountryCurrencyRequest
    {
        public List<MultipleCurrencyRequest> Currencies { get; set; }
    }

    public class UpdateCountryCurrencyRequest : MultipleCurrencyRequest
    {
        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The CountryIso3 field must be exactly 3 characters long.")]
        public string Country3Iso { get; set; } = null!;

        [Required]
        public int Id { get; set; }
    }

}
