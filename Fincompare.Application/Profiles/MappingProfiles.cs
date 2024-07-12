using AutoMapper;
using Fincompare.Application.Request;
using Fincompare.Application.Request.CountryRequest;
using Fincompare.Application.Request.CurrencyRequest;
using Fincompare.Domain.Entities;
using Fincompare.Domain.Entities.UserManagementEntities;
using static Fincompare.Application.Request.CountryCurrencyRequest.CountryCurrencyBaseModel;
using static Fincompare.Application.Request.CurrencyRequest.CurrencyRequests;
using static Fincompare.Application.Response.CountryCurrencyResponse.CountryCurrencyResponseBaseClass;
using static Fincompare.Application.Response.CurrencyResponse.CurrencyResponseBaseModel;

namespace Fincompare.Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterUserRequest, User>().ReverseMap();
            CreateMap<CountryRequest, Country>().ReverseMap();
            CreateMap<GetCountryDto, Country>().ReverseMap();
            CreateMap<AddCurrencyRequests, Currency>().ReverseMap();
            CreateMap<UpdateCurrencyRequests, Currency>().ReverseMap();
            CreateMap<GetCurrencyResponse, Currency>().ReverseMap();
            CreateMap<AddCountryCurrencyRequest, CountryCurrency>().ReverseMap();
            CreateMap<UpdateCountryCurrency, CountryCurrency>().ReverseMap();
            CreateMap<GetAllCountryCurrencyResponse, CountryCurrency>().ReverseMap();

        }
    }
}
