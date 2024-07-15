using Fincompare.Application.Repositories;
using Fincompare.Application.Services;
using Fincompare.Domain.Entities;
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

            return services;
        }
    }
}
