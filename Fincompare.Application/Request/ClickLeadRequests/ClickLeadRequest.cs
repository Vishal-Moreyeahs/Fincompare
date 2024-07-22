using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Request.ClickLeadRequests
{
    public class ClickLeadRequest
    {
        public int? CustomerUserId { get; set; }

        [Required]
        public string Country3Iso { get; set; } = null!;

        [Required]
        public int MerchantId { get; set; }

        [Required]
        public string RoutingParamters { get; set; } = null!;

        public DateTime Date { get; set; }
    }

    public class AddClickLeadRequest : ClickLeadRequest { }
    public class UpdateClickLeadRequest : ClickLeadRequest {
        [Required]
        public int Id { get; set; }
    }
}
