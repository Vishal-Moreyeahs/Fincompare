using Fincompare.Application.Contracts.Infrastructure;
using Fincompare.Application.Models;
using Fincompare.Infrastructure.Authentication;
using Fincompare.Infrastructure.BackgroundServices;
using Fincompare.Infrastructure.RateServices;
using Fincompare.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Fincompare.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHostedService<MarketRateBackgroundService>();

            //services.AddSingleton<MarketRateBackgroundService>();
            // Corrected code: Check if JwtSettings is present in your configuration
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            //services.AddSingleton<IHostedService, MarketRateBackgroundService>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
            
            

            services.AddTransient<IExchangeRate, ExchangeRate>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ICryptographyService, CryptographyService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IUserManagerServices, UserManagerServices>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                // Corrected code: Use the options.TokenValidationParameters to configure JWT settings
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration.GetValue<string>("JwtSettings:Issuer"),
                    ValidAudience = configuration.GetValue<string>("JwtSettings:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtSettings:Key")))
                };
            });

            return services;
        }

    }
}
