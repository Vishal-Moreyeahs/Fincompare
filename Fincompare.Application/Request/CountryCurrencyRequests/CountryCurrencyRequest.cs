namespace Fincompare.Application.Request.CountryCurrencyRequests
{
    public class CountryCurrencyRequest
    {

        public string Country3Iso { get; set; } = null!;

    }


    public class MultipleCurrencyRequest
    {
        public string CurrencyIso { get; set; }
        public bool IsPrimaryCur { get; set; } = false;
        public int? CountryCurrencyCategoryId { get; set; }
        public bool Status { get; set; } = true;

    }

    public class UpdateCountryWithMultipleCurrencyRequest : CountryCurrencyRequest
    {
        public List<MultipleCurrencyRequest> Currencies { get; set; }
    }

    public class UpdateCountryCurrencyRequest : CountryCurrencyRequest
    {
        public int Id { get; set; }
    }
}
