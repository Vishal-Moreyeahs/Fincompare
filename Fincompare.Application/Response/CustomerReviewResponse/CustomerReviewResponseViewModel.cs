using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Response.CustomerReviewResponse
{
    public class CustomerReviewResponseViewModel
    {
        public int Id { get; set; }

        public int MerchantId { get; set; }

        public string MerchantName { get; set; }
        public string Review { get; set; } = null!;

        public int Rating { get; set; }

        public bool Status { get; set; }
    }
}
