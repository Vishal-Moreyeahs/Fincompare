namespace Fincompare.Application.Request.StateRequest
{
    public class StateDTO
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        public bool Status { get; set; }
        public string Country3Iso { get; set; }
        //public CountryDTO Country { get; set; }
        //public List<CityDTO> Cities { get; set; }
    }
}
