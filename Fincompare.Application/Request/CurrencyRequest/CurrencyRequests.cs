using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.CurrencyRequest
{
    public class CurrencyRequests
    {
        public class AddCurrencyRequests
        {
            [Required]
            public string CurrencyName { get; set; } = string.Empty;

            [Required]
            public string? CurrencyIso { get; set; }

            [Required]
            public int Decimal { get; set; }

            [Required]
            public int VolatilityRange { get; set; }
            public bool Status { get; set; } = true;
        }

        public class UpdateCurrencyRequests : AddCurrencyRequests
        {
            //public int Id { get; set; }

        }

    }
}
