using AutoMapper;
using Fincompare.Application.Request;
using Fincompare.Application.Request.CityRequest;
using Fincompare.Application.Request.CountryCurrencyRequests;
using Fincompare.Application.Request.CountryRequest;
using Fincompare.Application.Request.MarketRateRequest;
using Fincompare.Application.Request.MerchantProductRequests;
using Fincompare.Application.Request.MerchantRemitProductRateRequests;
using Fincompare.Application.Request.MerchantRequests;
using Fincompare.Application.Request.StateRequest;
using Fincompare.Application.Response.MerchantProductResponse;
using Fincompare.Application.Response.MerchantRemitProductRateResponse;
using Fincompare.Domain.Entities;
using Fincompare.Domain.Entities.UserManagementEntities;
using static Fincompare.Application.Request.CouponRequest.CouponRequestViewModel;
using static Fincompare.Application.Request.CurrencyRequest.CurrencyRequests;
using static Fincompare.Application.Request.CustomerUsedCouponRequest.CustomerUsedCouponViewRequest;
using static Fincompare.Application.Request.GroupMerchantRequest.GroupMerchantBaseModel;
using static Fincompare.Application.Request.InstrumentRequest.InstrumentRequestBaseModel;
using static Fincompare.Application.Request.MerchantProductCouponRequest.MerchantProductRequestViewModel;
using static Fincompare.Application.Request.MerchantRemitProductFeeRequests.MerchantRemitProductFeeRequestViewModel;
using static Fincompare.Application.Request.ProductRequests.ProductRequestViewModel;
using static Fincompare.Application.Request.ServiceCategoriesRequest.ServiceCategoriesViewModel;
using static Fincompare.Application.Response.CouponResponse.CouponResponseViewModel;
using static Fincompare.Application.Response.CurrencyResponse.CurrencyResponseBaseModel;
using static Fincompare.Application.Response.CustomerUsedCouponResponse.CustomerUsedCouponViewResponse;
using static Fincompare.Application.Response.GroupMerchantResponse.GroupMerchantViewResponse;
using static Fincompare.Application.Response.InstrumentResponse.InstrumentResponseBaseClass;
using static Fincompare.Application.Response.MerchantProductCouponResponse.MerchantProductCouponViewResponse;
using static Fincompare.Application.Response.ProductResponse.ProductResponseBaseClass;
using static Fincompare.Application.Response.ServiceCategoriesResponse.ServiceCategoriesViewResponse;

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
            CreateMap<IEnumerable<StateDTO>, State>().ReverseMap();
            CreateMap<CityDto, City>().ReverseMap();
            CreateMap<AddCityRequest, City>().ReverseMap();
            CreateMap<UpdateCityRequest, City>().ReverseMap();
            CreateMap<UpdateCountryCurrencyRequest, CountryCurrency>().ForMember(dest => dest.CountryCurrencyCategoryId, opt => opt.MapFrom(src => src.Category)).ReverseMap();
            CreateMap<CountryCurrencyDto, CountryCurrency>().ReverseMap();
            CreateMap<AddMarketRate, MarketRate>().ReverseMap();
            CreateMap<UpdateMarketRate, MarketRate>().ReverseMap();
            CreateMap<MarketRateDto, MarketRate>().ReverseMap();
            CreateMap<AddGroupMerchantRequestClass, GroupMerchant>().ReverseMap();
            CreateMap<GetAllGroupMerchantResponse, GroupMerchant>().ReverseMap();
            CreateMap<MerchantDto, Merchant>().ReverseMap();
            CreateMap<AddMerchantRequest, Merchant>().ReverseMap();
            CreateMap<UpdateMerchantRequest, Merchant>().ReverseMap();
            CreateMap<CreateInstrumentRequest, Instrument>().ReverseMap();
            CreateMap<UpdateInstrumentRequest, Instrument>().ReverseMap();
            CreateMap<GetAllInstrumentResponse, Instrument>().ReverseMap();
            CreateMap<CreateProductRequest, Product>().ReverseMap();
            CreateMap<UpdateProductRequest, Product>().ReverseMap();
            CreateMap<GetAllProductResponse, Product>().ReverseMap();
            CreateMap<GetAllProductResponse, Product>().ReverseMap();
            CreateMap<MerchantProductDto, MerchantProduct>().ReverseMap();
            CreateMap<AddMerchantProductRequest, MerchantProduct>().ReverseMap();
            CreateMap<UpdateMerchantProductRequest, MerchantProduct>().ReverseMap();
            CreateMap<MerchantProductViewModel, MerchantProduct>().ReverseMap();
            CreateMap<CreateMerchantRemitProductFeeRequest, MerchantRemitProductFee>().ReverseMap();
            CreateMap<UpdateMerchantRemitProductFeeRequest, MerchantRemitProductFee>().ReverseMap();
            CreateMap<MerchantRemitProductRateViewModel, MerchantRemitProductRate>().ReverseMap();
            CreateMap<UpdateMerchantRemitProductRateRequest, MerchantRemitProductRate>().ReverseMap();
            CreateMap<AddMerchantRemitProductRateRequest, MerchantRemitProductRate>().ReverseMap();


            CreateMap<CreateServiceCategoriesRequest, ServiceCategory>().ForMember(desc => desc.ServCategoryName, (req => req.MapFrom(src => src.ServiceCategoryName))).ReverseMap();
            CreateMap<UpdateServiceCategoriesRequest, ServiceCategory>().ForMember(desc => desc.ServCategoryName, (req => req.MapFrom(src => src.ServiceCategoryName))).ReverseMap();
            CreateMap<GetAllServiceCategoriesResponse, ServiceCategory>().ForMember(desc => desc.ServCategoryName, (req => req.MapFrom(src => src.ServiceCategoryName))).ReverseMap();


            CreateMap<CreateCouponRequest, Coupon>().ReverseMap();
            CreateMap<UpdateCouponRequest, Coupon>().ReverseMap();
            CreateMap<FetchCouponResponse, Coupon>().ReverseMap();

            CreateMap<CreateMerchantProductCouponRequest, MerchantProductCoupon>().ReverseMap();
            CreateMap<UpdateMerchantProductCouponRequest, MerchantProductCoupon>().ReverseMap();
            CreateMap<GetAllMerchantProductCouponResponse, MerchantProductCoupon>().ForMember(desc => desc.Id, (req => req.MapFrom(src => src.MerchantCouponId))).ReverseMap();
            CreateMap<UpdateMerchantCouponResponseClass, MerchantProductCoupon>().ReverseMap();

            CreateMap<CreateCustomerUsedCouponRequest, CustomerUsedCoupon>()
                .ForMember(desc => desc.MerchantProductCouponId, (req => req.MapFrom(src => src.MerchantCouponId))).ReverseMap()
                .ForMember(desc => desc.CustomerId, (req => req.MapFrom(src => src.CustomerUserId))).ReverseMap();

            CreateMap<GetAllCustomerUsedCouponResponse, CustomerUsedCoupon>()
                 .ForMember(desc => desc.MerchantProductCouponId, (req => req.MapFrom(src => src.MerchantCouponId))).ReverseMap()
                 .ForMember(desc => desc.CustomerId, (req => req.MapFrom(src => src.CustomerUserId))).ReverseMap()
                 .ForMember(desc => desc.Id, (req => req.MapFrom(src => src.CustomerUsedCouponId))).ReverseMap();


        }
    }
}
