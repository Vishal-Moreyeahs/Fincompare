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
            public string Country3Iso { get; set; } = null!;
            [Required]
            public string InstrumentType { get; set; } = null!;
            public bool Status { get; set; } 
        }

        public class UpdateInstrumentRequest : CreateInstrumentRequest
        {
            [Required]
            public int Id { get; set; }
        }
    }
}
