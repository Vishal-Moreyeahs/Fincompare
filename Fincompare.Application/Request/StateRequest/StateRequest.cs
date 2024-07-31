using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.StateRequest
{
    public class StateRequest
    {
        [Required]
        [MinLength(3)]
        public string StateName { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The CountryIso3 field must be exactly 3 characters long.")]
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
