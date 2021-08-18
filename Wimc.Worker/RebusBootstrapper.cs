using System;
using System.Threading.Tasks;
using Rebus.ServiceProvider;
using Wimc.Domain.Messages.Commands;

namespace Wimc.Worker
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
            _serviceProvider.UseRebus(async (bus) => 
            {
                await bus.Subscribe<AddResource>().ConfigureAwait(false);
            });
            return true;
        }

        public bool Stop()
        {
            return true;
        }
    }
}