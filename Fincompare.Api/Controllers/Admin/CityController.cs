using Fincompare.Application.Repositories;
using Fincompare.Application.Request.CityRequest;
using Fincompare.Domain.Enums;
using Fincompare.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Fincompare.Api.Controllers.Admin
{

    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityServices _cityServices;

        public CityController(ICityServices cityServices)
        {
            _cityServices = cityServices;
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPost]
        [Route("add-city")]
        public async Task<IActionResult> AddCity(AddCityRequest model)
        {
            if (!ModelState.IsValid)
            {
                var validationErrors = ModelState.Keys
                .SelectMany(key => ModelState[key].Errors.Select(x => new
                {
                    Field = key,
                    Error = x.ErrorMessage
                }))
                .ToList();
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            var response = await _cityServices.AddCity(model);
            return Ok(response);
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPut]
        [Route("update-city")]
        public async Task<IActionResult> UpdateCity(UpdateCityRequest model)
        {
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            var response = await _cityServices.UpdateCity(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("fetch-all-cities")]
        public async Task<IActionResult> GetAllCity(string? countryIso3, int? StateId, int? CityId, bool? Status)
        {
            if (!ModelState.IsValid)
            {
                // Return a 400 Bad Request response with validation errors
                return BadRequest(ModelState);
            }
            var response = await _cityServices.GetAllCity(countryIso3, StateId, CityId, Status);
            return Ok(response);
        }


        //[HttpGet]
        //[Route("get-city-by-stateid")]
        //public async Task<IActionResult> GetCityByStateId(int stateId)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        // Return a 400 Bad Request response with validation errors
        //        return BadRequest(ModelState);
        //    }
        //    var response = await _cityServices.GetCityByStateId(stateId);
        //    return Ok(response);
        //}

        //[HttpGet]
        //[Route("get-by-cityid")]
        //public async Task<IActionResult> GetByCityId(int cityId)
        //{
        //    var response = await _cityServices.GetCityByStateId(cityId);
        //    return Ok(response);
        //}

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpDelete]
        [Route("delete-city")]
        public async Task<IActionResult> DeleteCity(int cityId)
        {
            var response = await _cityServices.DeleteCity(cityId);
            return Ok(response);
        }

    }
}
