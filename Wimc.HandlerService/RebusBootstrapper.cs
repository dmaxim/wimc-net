using System;
using Rebus.ServiceProvider;
using Wimc.Domain.Messages.Commands;

namespace Wimc.HandlerService
{
    public class RebusBootstrapper
    {

        private readonly IServiceProvider _serviceProvider;

        public RebusBootstrapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public bool Start()
        {
            _serviceProvider.UseRebus(bus =>
            {
                bus.Subscribe<AddResource>().ConfigureAwait(false).GetAwaiter().GetResult();
            });
            return true;
        }

        public bool Stop()
        {
            return true;
        }
    }
}