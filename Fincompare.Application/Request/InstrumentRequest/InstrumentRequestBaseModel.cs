namespace Fincompare.Application.Request.InstrumentRequest
{
    public class InstrumentRequestBaseModel
    {
        public class CreateInstrumentRequest
        {

            public string InstrumentName { get; set; } = null!;
            public string Country3Iso { get; set; } = null!;
            public bool Status { get; set; }
        }

        public class UpdateInstrumentRequest : CreateInstrumentRequest
        {
            public int Id { get; set; }
        }
    }
}
