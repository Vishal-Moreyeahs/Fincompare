using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.InstrumentRequest
{
    public class InstrumentRequestBaseModel
    {
        public class CreateInstrumentRequest
        {

            [Required]
            public string InstrumentName { get; set; } = null!;


            [Required]
            [RegularExpression("^(?i)(Payin|Payout)$", ErrorMessage = "InstrumentType must be either 'Payin' or 'Payout'.")]
            [DisplayName("Payin/Payout")]
            public string InstrumentType { get; set; } = null!;

            public bool Status { get; set; } = true;
        }

        public class UpdateInstrumentRequest : CreateInstrumentRequest
        {
            [Required]
            public int Id { get; set; }
        }
    }
}
