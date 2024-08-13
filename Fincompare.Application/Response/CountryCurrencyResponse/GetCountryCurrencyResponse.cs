namespace Fincompare.Application.Response.CountryCurrencyResponse
{
    public class GetCountryCurrencyResponse
    {
        public int Id { get; set; }
        public string Country3Iso { get; set; }
        public string CountryCode { get; set; }

        public string CurrencyIso { get; set; }
        public bool IsPrimary { get; set; }
        public string Category { get; set; }
        public bool Status { get; set; }
        public bool IsDefault { get; set; } = false;
    }
}
