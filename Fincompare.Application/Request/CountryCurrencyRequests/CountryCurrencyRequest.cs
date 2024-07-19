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
        public string CurrencyIso { get; set; }
        public bool IsPrimary { get; set; } = false;
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
        public string Country3Iso { get; set; } = null!;

        [Required]
        public int Id { get; set; }
    }

}
