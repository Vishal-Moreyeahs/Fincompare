using Fincompare.Application.Contracts.Infrastructure;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Fincompare.Infrastructure.BackgroundServices
{
    public class MarketRateBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly MarketRateSettings _marketRateSettings;

        //private readonly IExchangeRate _exchangeRate;

        public MarketRateBackgroundService(/*IExchangeRate exchangeRate,*/ IServiceScopeFactory serviceScopeFactory, IOptions<MarketRateSettings> marketRateSettings)
        {
            //_exchangeRate = exchangeRate;
            _marketRateSettings = marketRateSettings.Value;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var periodicTimer = new PeriodicTimer(TimeSpan.FromHours(_marketRateSettings.ScheduledTimePeriodInHours));

            try
            {
                while (await periodicTimer.WaitForNextTickAsync(stoppingToken))
                {
                    try
                    {
                        using (var scope = _serviceScopeFactory.CreateScope())
                        {
                            var exchangeRate = scope.ServiceProvider.GetRequiredService<IExchangeRate>();
                            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                            Console.WriteLine("Run Background Process");
                            await exchangeRate.UpdateDbCurrencyExchangeRates();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (OperationCanceledException)
            {

            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }
    }


}
