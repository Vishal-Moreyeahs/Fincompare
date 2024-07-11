using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Request.CityRequest
{
    public class CityRequest
    {
        public string CityName { get; set; } = null!;

        public int StateId { get; set; }
        public bool Status { get; set; } = true;
    }

    public class AddCityRequest : CityRequest { }

    public class UpdateCityRequest : CityRequest
    {
        public int Id { get; set; }
    }
}
