namespace Fincompare.Application.Request.CityRequest
{
    public class CityRequest
    {
        public string CityName { get; set; } = null!;

        public int StateId { get; set; }
    }

    public class AddCityRequest : CityRequest { }

    public class UpdateCityRequest : CityRequest
    {
        public int Id { get; set; }
    }
}
