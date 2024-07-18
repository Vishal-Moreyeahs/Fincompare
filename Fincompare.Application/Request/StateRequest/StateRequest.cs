using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.StateRequest
{
    public class StateRequest
    {
        [Required]
        public string StateName { get; set; }
        [Required]
        public string Country3Iso { get; set; } = null!;
        public bool Status { get; set; } = true;
    }

    public class AddStateRequest : StateRequest { }

    public class UpdateStateRequest : StateRequest
    {
        [Required]
        public int Id { get; set; }
    }



}
