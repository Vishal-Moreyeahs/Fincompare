﻿using Fincompare.Application.Request.CountryRequest;
using Fincompare.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Repositories
{
    public interface ICountryServices
    {
        Task<ApiResponse<string>> AddCountry(CountryRequest addCountry);
        Task<ApiResponse<string>> RemoveCountry(string country3Iso);
        Task<ApiResponse<string>> UpdateCountry(CountryRequest request);
        Task<ApiResponse<GetCountryDto>> GetCountryByCountryName(string countryName);
        Task<ApiResponse<List<GetCountryDto>>> GetAllCountry();
    }
}
