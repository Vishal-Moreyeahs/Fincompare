using Fincompare.Application.Contracts.Infrastructure;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Models.RateModel;
using Fincompare.Application.Repositories;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities;
using Newtonsoft.Json;

namespace Fincompare.Infrastructure.RateServices
{
    public class ExchangeRate : IExchangeRate
    {
        private readonly ICurrencyServices _currencyServices;
        private readonly IUnitOfWork _unitOfWork;

        public ExchangeRate(ICurrencyServices currencyServices, IUnitOfWork unitOfWork) { 
            _currencyServices = currencyServices;
            _unitOfWork = unitOfWork;
        }
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

        public async Task<ApiResponse<List<string>>> UpdateDbCurrencyExchangeRates()
        {
            List<string> failCurrency = [];
            try
            {
                var getAllCurrencyCode = (await _currencyServices.GetAllCurrency())
                    .Data.Select(x => x.CurrencyIso).OrderBy(x => x).ToArray();
                foreach (var currencyCode in getAllCurrencyCode)
                {
                    DateTime currentDateTime = DateTime.UtcNow;
                    var conversionData = Import(currencyCode);
                    if (conversionData == null)
                    {
                        failCurrency.Add(currencyCode);
                        continue;
                    }
                    var addToDb = conversionData.conversion_rates
                        .Where(x => getAllCurrencyCode.Contains(x.Key))
                        .Select(x => new MarketRate
                        {
                            SendCur = currencyCode,
                            ReceiveCur = x.Key,
                            Rate = x.Value,
                            Date = currentDateTime,
                            RateSource = "Third-Party",
                        })
                    .ToList();

                    await _unitOfWork.GetRepository<MarketRate>().AddRange(addToDb);
                    await _unitOfWork.SaveChangesAsync();
                }
                if (failCurrency.Count != 0)
                    return new ApiResponse<List<string>>()
                    {
                        Message = "Some Currency Not Update",
                        Data = failCurrency,
                    };
                return new ApiResponse<List<string>>()
                {
                    Message = "Success",
                    Status = true,
                    Data = failCurrency,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
