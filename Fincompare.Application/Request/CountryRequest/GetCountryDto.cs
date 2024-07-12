using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Request.CountryRequest
{
    public class GetCountryDto
    {
        public string Country3Iso { get; set; }
        public string Country2Iso { get; set; }
        public string CountryName { get; set; } = null!;
        public bool Status { get; set; }

        public string? WebLink { get; set; }
    }
}
