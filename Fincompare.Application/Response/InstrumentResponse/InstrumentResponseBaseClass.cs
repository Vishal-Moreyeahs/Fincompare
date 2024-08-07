namespace Fincompare.Application.Response.InstrumentResponse
{
    public class InstrumentResponseBaseClass
    {
        public class GetAllInstrumentResponse
        {
            public int Id { get; set; }
            public string InstrumentName { get; set; } = null!;
            public string InstrumentType { get; set; } = null!;
            public bool Status { get; set; }
        }
    }
}
