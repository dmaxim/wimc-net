using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wimc.Worker;

namespace WImc.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RebusBootstrapper _bootstrapper;
        
        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _bootstrapper = new RebusBootstrapper(serviceProvider);

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            _bootstrapper.Start();
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(5000, stoppingToken);
            }


        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _bootstrapper.Stop();
            return base.StopAsync(cancellationToken);
        }
    }
}