using AutoMapper;
using Fincompare.Application.Request;
using Fincompare.Application.Request.CountryRequest;
using Fincompare.Domain.Entities;
using Fincompare.Domain.Entities.UserManagementEntities;

namespace Fincompare.Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterUserRequest, User>().ReverseMap();
            CreateMap<CountryRequest, Country>().ReverseMap();
            CreateMap<GetCountryDto, Country>().ReverseMap();
        }
    }
}
