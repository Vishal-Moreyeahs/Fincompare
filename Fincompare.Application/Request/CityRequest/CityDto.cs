using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Request.CityRequest
{
    public class CityDto
    {
        public int Id { get; set; }

        public string CityName { get; set; } = null!;

        public int StateId { get; set; }
    }
}
