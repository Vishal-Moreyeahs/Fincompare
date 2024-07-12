namespace Fincompare.Application.Request.CountryCurrencyRequest
{
    public class CountryCurrencyBaseModel
    {
        public class AddCountryCurrencyRequest
        {

            public string Country3Iso { get; set; } = null!;
            public int CurrencyId { get; set; }
            public bool IsPrimaryCur { get; set; }
            public int? CountryCurrencyCategoryId { get; set; }
            public bool Status { get; set; }
        }

        public class UpdateCountryCurrency : AddCountryCurrencyRequest
        {
            public int Id { get; set; }
        }
    }
}
