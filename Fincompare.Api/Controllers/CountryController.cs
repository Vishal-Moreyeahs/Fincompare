using Fincompare.Application.Contracts.Persistence;
using Fincompare.Domain.Entities;
using Fincompare.Domain.Enums;
using Fincompare.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Fincompare.Api.Controllers
{
    [HasPermission(PermissionEnum.CanAccessAdmin)]
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

            foreach (var currency in list)
            {
                if (currency.Currencies != null && currency.Currencies.Count != 0)
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

        [HttpPost]
        [Route("Add-State")]
        public async Task<IActionResult> AddBulkStates()
        {
            var list = GetWithState();

            await UpdateStatesAsync(list);

            return Ok("insertData success");
        }

        private async Task UpdateStatesAsync(List<UpdateStateModel> updateStateModels)
        {
            try
            {
                foreach (var updateStateModel in updateStateModels)
                {
                    foreach (var state2 in updateStateModel.States)
                    {
                        // Check if the state exists in the database
                        //var state = await _unitOfWork.GetRepository<>
                        //var state = await _context.States
                        //    .FirstOrDefaultAsync(s => s.StateName == state2.Name && s.Country3Iso == updateStateModel.Iso3);

                        //if (state == null)
                        //{
                        // If the state does not exist, create a new one
                        if (updateStateModel.States != null && updateStateModel.States.Count > 0)
                        {
                            var state = new State
                            {
                                StateName = state2.Name,
                                Country3Iso = updateStateModel.Iso3,
                                Status = true
                            };
                            await _unitOfWork.GetRepository<State>().Add(state);
                            await _unitOfWork.SaveChangesAsync(); // Save to get the State Id
                                                                  //}
                            if (state2.Cities != null && state2.Cities.Count > 0)
                            {
                                foreach (var city2 in state2.Cities)
                                {
                                    // Check if the city exists in the database
                                    //var city = await _context.Cities
                                    //    .FirstOrDefaultAsync(c => c.CityName == city2.Name && c.StateId == state.Id);

                                    //if (city == null)
                                    //{
                                    // If the city does not exist, create a new one
                                    var city = new City
                                    {
                                        CityName = string.IsNullOrEmpty(city2.Name) ? "" : city2.Name,
                                        StateId = state.Id,
                                        Status = true
                                    };
                                    await _unitOfWork.GetRepository<City>().Add(city);
                                    //}
                                    //else
                                    //{
                                    //    // Update the city if it already exists
                                    //    city.CityName = city2.Name;
                                    //    city.Status = true;
                                    //    _context.Cities.Update(city);
                                    //}
                                }
                            }
                        }


                        await _unitOfWork.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

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

        private List<UpdateStateModel> GetWithState()
        {
            string baseURL = "https://raw.githubusercontent.com/dr5hn/countries-states-cities-database/master/countries%2Bstates%2Bcities.json";
            string response = RestClientHelper.HitTheRestClient(baseURL);
            var myDeserializedClass = JsonConvert.DeserializeObject<List<UpdateStateModel>>(response);
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



        public class City2
        {
            public string Name { get; set; }
        }

        public class UpdateStateModel
        {
            public string Iso3 { get; set; }
            public List<State2> States { get; set; }
        }

        public class State2
        {
            public string Name { get; set; }
            public List<City2> Cities { get; set; }
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
