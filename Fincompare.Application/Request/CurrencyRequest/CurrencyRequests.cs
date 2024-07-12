namespace Fincompare.Application.Request.CurrencyRequest
{
    public class CurrencyRequests
    {
        public class AddCurrencyRequests
        {
            public string CurrencyName { get; set; } = string.Empty;
            public string? CurrencyIso { get; set; }
            public int Decimal { get; set; }
            public int VolatilityRange { get; set; }
        }

        public class UpdateCurrencyRequests : AddCurrencyRequests
        {
            public int Id { get; set; }
        }

    }
}
