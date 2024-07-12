namespace Fincompare.Application.Response.CountryCurrencyResponse
{
    public class CountryCurrencyResponseBaseClass
    {
        public class GetAllCountryCurrencyResponse
        {
            public int Id { get; set; }
            public string Country3Iso { get; set; } = null!;
            public int CurrencyId { get; set; }
            public bool IsPrimaryCur { get; set; }
            public int? CountryCurrencyCategoryId { get; set; }
            public bool Status { get; set; }
        }
    }
}
