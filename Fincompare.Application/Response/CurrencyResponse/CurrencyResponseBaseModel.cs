using Fincompare.Domain.Entities;

namespace Fincompare.Application.Response.CurrencyResponse
{
    public class CurrencyResponseBaseModel
    {
        public class GetAllCurrencyResponse
        {
            public int Id { get; set; }
            public string CurrencyName { get; set; } = null!;
            public string? CurrencyIso { get; set; }
            public int Decimal { get; set; }
            public int VolatilityRange { get; set; }
        }

        public class GetCurrencyResponse : GetAllCurrencyResponse
        {
            
        }
    }
}
