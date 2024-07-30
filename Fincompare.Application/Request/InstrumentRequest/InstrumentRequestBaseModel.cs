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
            [StringLength(3, MinimumLength = 3, ErrorMessage = "The Country3Iso field must be exactly 3 characters long.")]
            public string Country3Iso { get; set; } = null!;

            [Required]
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
