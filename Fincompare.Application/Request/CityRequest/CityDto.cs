namespace Fincompare.Application.Request.CityRequest
{
    public class CityDto
    {
        public int Id { get; set; }

        public string CityName { get; set; } = null!;

        public int StateId { get; set; }
        public bool Status { get; set; }
    }
}
