using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wimc.Infrastructure.Configuration;

namespace Wimc.Handler
{
    internal static class HandlerServiceStartup
    {

        public static IServiceCollection AddHandlerServiceDependencies(this IServiceCollection collection, IConfiguration config)
        {
            collection.Configure<MessageBusConfiguration>(config.GetSection("MessageBus"));
            return collection;

        }
        
    }
}