﻿using Fincompare.Application.Repositories;
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
            services.AddTransient<ICountryCurrencyService, CountryCurrencyService>();

            return services;
        }
    }
}
