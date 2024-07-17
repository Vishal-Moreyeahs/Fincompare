using Fincompare.Application.Contracts.Persistence;
using Fincompare.Persitence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fincompare.Persitence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FincompareDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DBConnection")));

            //services.AddTransient<ICryptographyService,CryptographyService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
