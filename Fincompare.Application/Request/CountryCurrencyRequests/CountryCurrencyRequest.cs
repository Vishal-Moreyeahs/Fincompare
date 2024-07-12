using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Request.CountryCurrencyRequests
{
    public class CountryCurrencyRequest
    {

        public string Country3Iso { get; set; } = null!;

        public int CurrencyId { get; set; }

        public bool IsPrimaryCur { get; set; } = false;

        public int? CountryCurrencyCategoryId { get; set; }

    }

    public class AddCountryCurrencyRequest : CountryCurrencyRequest { }

    public class  UpdateCountryCurrencyRequest : CountryCurrencyRequest
    {
        public int Id { get; set; }
    }
}
