namespace Fincompare.Application.Models.RateModel
{
    public class API_Obj
    {
        public string result { get; set; }
        public string base_code { get; set; }
        public Dictionary<string, double> conversion_rates { get; set; }
    }
}
