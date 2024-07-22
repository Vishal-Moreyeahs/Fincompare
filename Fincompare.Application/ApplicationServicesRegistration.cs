using Fincompare.Application.Repositories;
using Fincompare.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Fincompare.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<ICountryServices, CountryServices>();
            services.AddTransient<ICurrencyServices, CurrencyServices>();
            services.AddTransient<IStateServices, StateServices>();
            services.AddTransient<ICityServices, CityServices>();
            services.AddTransient<ICountryCurrencyManager, CountryCurrencyManager>();
            services.AddTransient<IMarketRateServices, MarketRateServices>();
            services.AddTransient<IMerchantServices, MerchantServices>();
            services.AddTransient<IGroupMerchantService, GroupMerchantService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IInstrumentService, InstrumentService>();
            services.AddTransient<IMerchantProductService, MerchantProductService>();
            services.AddTransient<IMerchantRemitFee, MerchantRemitFee>();
            services.AddTransient<IMerchantRemitProductRateService, MerchantRemitProductRateService>();
            services.AddTransient<IServiceCategory, ServiceCategories>();
            services.AddTransient<ICouponService, CouponServices>();
            services.AddTransient<IMerchantProductCouponService, MerchantProductCouponService>();
            services.AddTransient<ICustomerUsedCouponService, CustomerUsedCouponService>();

            services.AddTransient<IMerchantCompaignServices, MerchantCompaignServices>();
            services.AddTransient<IRateCardServices, RateCardServices>();
            services.AddTransient<IClickLeadService, ClickLeadService>();
            
            return services;
        }
    }
}
