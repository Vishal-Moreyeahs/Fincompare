namespace Fincompare.Application.Request.StateRequest
{
    public class StateRequest
    {
        public string StateName { get; set; }
        public string Country3Iso { get; set; } = null!;
        public bool Status { get; set; } = true;
    }

    public class AddStateRequest : StateRequest { }

    public class UpdateStateRequest : StateRequest
    {
        public int Id { get; set; }
    }



}
