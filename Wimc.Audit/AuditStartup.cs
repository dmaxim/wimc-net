using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mx.EntityFramework.Contracts;
using Wimc.Audit.Handlers;
using Wimc.Business.Managers;
using Wimc.Data.Clients;
using Wimc.Data.Contexts;
using Wimc.Data.Repositories;
using Wimc.Domain.AppConfiguration;
using Wimc.Domain.Clients;
using Wimc.Domain.Repositories;
using Wimc.Eventing.Configuration;
using Wimc.Eventing.Repositories;

namespace Wimc.Audit
{
    internal static class AuditStartup
    {

        public static IServiceCollection AddHandlerServiceDependencies(this IServiceCollection collection, IConfiguration config)
        {
            collection.Configure<EventClientConfiguration>(config.GetSection("Events"));
            
            
            collection.AddTransient<IEventRepository, EventRepository>(provider =>
            {
                var configuration = provider.GetRequiredService<IOptions<EventClientConfiguration>>().Value;
                return new EventRepository(configuration);
            });

            
            collection.AddTransient<IAuditManager, AuditManager>();

            collection.Configure<DataStorageConfiguration>(config.GetSection("DataStorage"));
       
            collection.Configure<ArmApiClientConfig>(config.GetSection("ArmApiClientConfig"));

            
            collection.AddScoped<IEntityContext>(provider =>
            {
                var appConfig = provider.GetRequiredService<IOptions<DataStorageConfiguration>>().Value;
                    return new EntityContext(appConfig.WimcDatabase);    
                
            });

            collection.AddHttpClient<IApiClient, ArmApiClient>()
                .ConfigureHttpClient((provider, client) =>
                {
                    var configuration = provider.GetRequiredService<IOptions<ArmApiClientConfig>>().Value;
                    client.BaseAddress = new Uri(configuration.BaseUri);
                })
                .AddHttpMessageHandler(provider =>
                {
                    var configuration = provider.GetRequiredService<IOptions<ArmApiClientConfig>>().Value;
                    return new AzureAdTokenHandler(configuration);
                })
                .SetHandlerLifetime(TimeSpan.FromHours(1));

            collection.AddTransient<IResourceContainerRepository, ResourceContainerRepository>();
            collection.AddTransient<IResourceRepository, ResourceRepository>();
            collection.AddTransient<IResourceContainerManager, ResourceContainerManager>();

            collection.AddTransient<IMessageClient, FakeMessageClient>(); 
            collection.AddTransient<IMessageRepository, MessageRepository>();
            
            return collection;

        }
        
    }
}