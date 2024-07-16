using Fincompare.Application.Contracts.Infrastructure;
using Fincompare.Application.Models.RateModel;
using Newtonsoft.Json;

namespace Fincompare.Infrastructure.RateServices
{
    public class ExchangeRate : IExchangeRate
    {
        public API_Obj Import(string baseCur)
        {
            var API_KEY = "db0b704c87015ec8c4429397";
            API_Obj aPI_Obj = new API_Obj();
            try
            {

                String URLString = "https://v6.exchangerate-api.com/v6/" + API_KEY + "/latest/" + baseCur;
                using (var webClient = new System.Net.WebClient())
                {
                    var json = webClient.DownloadString(URLString);
                    aPI_Obj = JsonConvert.DeserializeObject<API_Obj>(json);
                }
                aPI_Obj.conversion_rates = aPI_Obj.conversion_rates.Where(x => x.Key != baseCur).ToDictionary();

                return aPI_Obj;
            }
            catch (Exception ex)
            {
                aPI_Obj.result = "error";
                return null;
            }
        }
    }
}
