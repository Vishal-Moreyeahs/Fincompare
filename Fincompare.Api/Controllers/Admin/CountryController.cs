﻿using Fincompare.Api.Middleware;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request.CountryRequest;
using Fincompare.Domain.Enums;
using Fincompare.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Fincompare.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryServices _countryServices;

        public CountryController(ICountryServices countryServices)
        {
            _countryServices = countryServices;
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpPost]
        [ValidateModelState]
        [Route("add")]
        public async Task<IActionResult> CreateCountry(CountryRequest request)
        {
            return Ok(await _countryServices.AddCountry(request));
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [ValidateModelState]
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateCountry(CountryRequest request)
        {
            return Ok(await _countryServices.UpdateCountry(request));
        }

        [HasPermission(PermissionEnum.CanAccessAdmin)]
        [HttpDelete]
        [Route("remove")]
        public async Task<IActionResult> DeleteCountryById(string country3iso)
        {
            return Ok(await _countryServices.RemoveCountry(country3iso));
        }

        [HttpGet]
        [Route("fetch-all-countries")]
        public async Task<IActionResult> GetAllCountry(string? country3iso, bool? status)
        {
            return Ok(await _countryServices.GetAllCountry(country3iso, status));
        }

        //[HttpGet]
        //[Route("get-by-country3Iso")]
        //public async Task<IActionResult> GetCountryById(string country3iso)
        //{
        //    return Ok(await _countryServices.GetCountryByCountryName(country3iso));
        //}
    }
}
