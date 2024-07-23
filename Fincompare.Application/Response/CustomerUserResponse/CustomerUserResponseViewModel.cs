using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Response.CustomerUserResponse
{
    public class CustomerUserResponseViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public string EmailId { get; set; }


        public string Address { get; set; } = string.Empty;

        public string Phone { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public string CountryId { get; set; }

        public string Password { get; set; } = string.Empty;
        public string RateSubscription { get; set; } = string.Empty;
        public string PromoSubscription { get; set; } = string.Empty;
        public string AuthProvider { get; set; } = string.Empty;
        public string AuthProviderId { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
    }
}
