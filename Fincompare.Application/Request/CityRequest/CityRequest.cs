using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.CityRequest
{
    public class CityRequest
    {
        [Required]
        public string CityName { get; set; } = null!;

        public int StateId { get; set; }
        public bool Status { get; set; } = true;
    }

    public class AddCityRequest : CityRequest { }

    public class UpdateCityRequest : CityRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
