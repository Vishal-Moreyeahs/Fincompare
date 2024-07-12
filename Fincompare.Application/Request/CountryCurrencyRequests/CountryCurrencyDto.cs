using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Request.CountryCurrencyRequests
{
    public class CountryCurrencyDto
    {
        public int Id { get; set; }

        public string Country3Iso { get; set; } = null!;

        public int CurrencyId { get; set; }

        public bool IsPrimaryCur { get; set; }

        public int? CountryCurrencyCategoryId { get; set; }

    }
}
