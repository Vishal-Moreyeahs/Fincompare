using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Request.StateRequest
{
    public class StateDTO
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        public string Country3Iso { get; set; }
        //public CountryDTO Country { get; set; }
        //public List<CityDTO> Cities { get; set; }
    }
}
