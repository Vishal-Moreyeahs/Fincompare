using AutoMapper;
using Fincompare.Application.Request;
using Fincompare.Application.Request.CityRequest;
using Fincompare.Application.Request.CountryCurrencyRequests;
using Fincompare.Application.Request.CountryRequest;
using Fincompare.Application.Request.MarketRateRequest;
using Fincompare.Application.Request.StateRequest;
using Fincompare.Domain.Entities;
using Fincompare.Domain.Entities.UserManagementEntities;
using static Fincompare.Application.Request.CurrencyRequest.CurrencyRequests;
using static Fincompare.Application.Request.GroupMerchantRequest.GroupMerchantBaseModel;
using static Fincompare.Application.Response.CurrencyResponse.CurrencyResponseBaseModel;
using static Fincompare.Application.Response.GroupMerchantResponse.GroupMerchantViewResponse;

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
            CreateMap<AddStateRequest, State>().ReverseMap();
            CreateMap<UpdateStateRequest, State>().ReverseMap();
            CreateMap<StateDTO, State>().ReverseMap();
            CreateMap<CityDto, City>().ReverseMap();
            CreateMap<AddCityRequest, City>().ReverseMap();
            CreateMap<UpdateCityRequest, City>().ReverseMap();
            CreateMap<UpdateCountryCurrencyRequest, CountryCurrency>().ReverseMap();
            CreateMap<CountryCurrencyDto, CountryCurrency>().ReverseMap();
            CreateMap<AddMarketRate, MarketRate>().ReverseMap();
            CreateMap<UpdateMarketRate, MarketRate>().ReverseMap();
            CreateMap<MarketRateDto, MarketRate>().ReverseMap();
            CreateMap<AddGroupMerchantRequestClass, GroupMerchant>().ReverseMap();
            CreateMap<GetAllGroupMerchantResponse, GroupMerchant>().ReverseMap();
        }
    }
}
