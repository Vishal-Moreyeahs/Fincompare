using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Response.CountryCurrencyResponse
{
    public class GetCountryCurrencyResponse
    {
        public int CountryCurrencyID { get; set; }
        public string CountryIso3 { get; set; }
        public string CountryIso2 { get; set; }
        public bool IsPrimary { get; set; }
        public string Category { get; set; }
        public bool Status { get; set; }
    }
}
