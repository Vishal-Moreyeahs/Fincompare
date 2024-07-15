using Fincompare.Application.Contracts.Persistence;
using Fincompare.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryController(IUnitOfWork unitOfWork)
        { 
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("add-all-countries")]
        public async Task<IActionResult> AddCountries()
        {
            var list = GetCountryWithCurrency();

            var insertingList = list.Select(x =>
                new Country
                {
                    Country3Iso = x.Alpha3Code,
                    Country2Iso = x.Alpha2Code,
                    CountryName = x.Name,
                    WebLink = x.Flag,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                });

            await _unitOfWork.GetRepository<Country>().AddRange(insertingList);
            await _unitOfWork.SaveChangesAsync();
            return Ok(insertingList);
        }

        [HttpPost]
        [Route("add-all-countries-with-currency")]
        public async Task<IActionResult> AddCountriesWithCurrency()
        {
            try
            {
                var list = GetCountryWithCurrency();

                var countryCurrencyList = new List<CountryCurrency>();

                var currencies = await _unitOfWork.GetRepository<Currency>().GetAll();

                foreach (var country in list)
                {
                    if (country.Currencies != null)
                    {
                        foreach (var currency in country.Currencies)
                        {
                            countryCurrencyList.Add(new CountryCurrency
                            {
                                Country3Iso = country.Alpha3Code,
                                CurrencyIso = currencies.Where(x => x.CurrencyIso == currency.Code).First().CurrencyIso,
                                CreatedDate = DateTime.UtcNow,
                                UpdatedDate = DateTime.UtcNow,
                                IsPrimaryCur = false,
                                Status = true
                            });
                        }
                    }
                }

                await _unitOfWork.GetRepository<CountryCurrency>().AddRange(countryCurrencyList);
                await _unitOfWork.SaveChangesAsync();
                return Ok("insertingList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpPost]
        [Route("add-all-currency")]
        public async Task<IActionResult> AddCurrency()
        {
            var list = GetWithCurrency();

            var newList = new List<Currency2>();

            foreach (var currency in list) {
                if(currency.Currencies != null && currency.Currencies.Count !=0)
                 newList.AddRange(currency.Currencies);
            }

            var insertData = newList
                .Select(x => new Currency
                {
                    CurrencyName = x.Name,
                    CurrencyIso = x.Code,
                    VolatilityRange = 4,
                    Decimal = 6,
                    Status = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                })
                .DistinctBy(x => x.CurrencyIso);


            await _unitOfWork.GetRepository<Currency>().AddRange(insertData);
            await _unitOfWork.SaveChangesAsync();
            return Ok("insertData success");
        }

        private List<Country2> GetCountryWithCurrency()
        {
            string baseURL = "https://restcountries.com/v2/all?fields=name,flag,currencies,alpha2Code,alpha3Code";
            string response = RestClientHelper.HitTheRestClient(baseURL);
            var myDeserializedClass = JsonConvert.DeserializeObject<List<Country2>>(response);
            return myDeserializedClass;
        }


        private List<CurrencyData> GetWithCurrency()
        {
            string baseURL = "https://restcountries.com/v2/all?fields=currencies";
            string response = RestClientHelper.HitTheRestClient(baseURL);
            var myDeserializedClass = JsonConvert.DeserializeObject<List<CurrencyData>>(response);
            return myDeserializedClass;
        }
        public class RestClientHelper
        {
            public static string HitTheRestClient(string baseURL)
            {
                var client = new RestClient(baseURL)
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.GET);
                var body = @"";

                request.AddParameter("text/plain", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                return response.Content;
            }
        }

        public class CurrencyData
        { 
            public List<Currency2> Currencies { get; set; }
        }

        public class Currency2
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Symbol { get; set; }
        }

        public class Country2
        {
            public string Name { get; set; }
            public List<Currency2> Currencies { get; set; }
            public string Alpha2Code { get; set; }
            public string Alpha3Code { get; set; }
            public string Country2Iso { get; set; }
            public string Flag { get; set; }
        }
    }
}
