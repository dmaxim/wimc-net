using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Wimc.Data.Clients;
using Wimc.Domain.Clients;
using Wimc.Infrastructure.Configuration;

namespace Wimc.Handler
{
    internal static class HandlerServiceStartup
    {

        public static IServiceCollection AddHandlerServiceDependencies(this IServiceCollection collection, IConfiguration config)
        {
            collection.Configure<MessageBusConfiguration>(config.GetSection("MessageBus"));
            
            /*
            collection.AddTransient<IMessageClient, MessageClient>(provider =>
            {
                var configuration = provider.GetService<IOptions<MessageBusConfiguration>>().Value;
                return new MessageClient(configuration.ConnectionString);
            });*/

            return collection;

        }
        
    }
}