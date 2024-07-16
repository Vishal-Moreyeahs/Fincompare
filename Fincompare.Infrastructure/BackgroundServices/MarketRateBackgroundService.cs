using Fincompare.Application.Repositories;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Infrastructure.BackgroundServices
{
    public class MarketRateBackgroundService : BackgroundService
    {
        private readonly IMarketRateServices _marketRateServices;
        public MarketRateBackgroundService(IMarketRateServices marketRateServices)
        {
            _marketRateServices = marketRateServices;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) { 
                
            }
            return Task.CompletedTask;
        }
    }
}
