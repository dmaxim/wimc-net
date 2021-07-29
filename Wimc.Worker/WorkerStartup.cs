using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mx.EntityFramework.Contracts;
using Mx.Library.ExceptionHandling;
using Rebus.Bus;
using Rebus.Config;
using Rebus.ServiceProvider;
using Wimc.Business.Managers;
using Wimc.Data.Clients;
using Wimc.Data.Contexts;
using Wimc.Data.Repositories;
using Wimc.Domain.AppConfiguration;
using Wimc.Domain.Clients;
using Wimc.Domain.Repositories;
using Wimc.Infrastructure.Configuration;
using Wimc.Infrastructure.Handlers;

namespace WImc.Worker
{
    public static class WorkerStartup
    {
            public static IServiceCollection AddWorkerDependencies(this IServiceCollection collection, IConfiguration config)
        {
            collection.Configure<MessageBusConfiguration>(config.GetSection("MessageBus"));
            collection.Configure<ArmApiClientConfig>(config.GetSection("ArmApiClientConfig"));
            
            collection.Configure<WimcUIConfiguration>(config.GetSection("WimcConfig"))
                .PostConfigure<WimcUIConfiguration>(options => options.ThrowIfInvalid());
            
            /*collection.AddTransient<IMessageClient, MessageClient>(provider =>
            {
                var configuration = provider.GetService<IOptions<MessageBusConfiguration>>().Value;
                return new MessageClient(configuration.ConnectionString);
            });*/
            
            collection.AddScoped<IEntityContext>(provider =>
            {
                var appConfig = provider.GetService<IOptions<WimcUIConfiguration>>()?.Value;
                if (appConfig != null)
                {
                    return new EntityContext(appConfig.DatabaseConnection);    
                }
                else
                {
                    throw new MxApplicationStateException("Unable to read the database connection string");
                }
            });
            


            collection.AddHttpClient<IApiClient, ArmApiClient>()
                .ConfigureHttpClient((provider, client) =>
                {
                    var configuration = provider.GetService<IOptions<ArmApiClientConfig>>().Value;
                    client.BaseAddress = new Uri(configuration.BaseUri);
                })
                .AddHttpMessageHandler(provider =>
                {
                    var configuration = provider.GetService<IOptions<ArmApiClientConfig>>().Value;
                    return new AzureAdTokenHandler(configuration);
                })
                .SetHandlerLifetime(TimeSpan.FromHours(1));
                
                
            
            collection.AddTransient<IResourceContainerRepository, ResourceContainerRepository>();
            collection.AddTransient<IResourceRepository, ResourceRepository>();
            collection.AddTransient<IResourceContainerManager, ResourceContainerManager>();
            collection.AddTransient<IResourceManager, ResourceManager>();
            collection.AddTransient<IMessageRepository, MessageRepository>();
            collection.AddTransient<IMessageClient, MessageClient>(provider =>
            {
                var configuration = provider.GetService<IOptions<MessageBusConfiguration>>().Value;
                var bus = provider.GetRequiredService<IBus>();
                return new MessageClient(configuration.ConnectionString, bus);
            });


            collection.AutoRegisterHandlersFromAssembly(Assembly.GetExecutingAssembly().FullName);

            collection.AddRebus((configurer, provider) =>
            {
                var busConfig = provider.GetService<IOptions<MessageBusConfiguration>>().Value;
                return configurer
                    .Transport(t => t.UseAzureServiceBus(busConfig.ConnectionString, busConfig.QueueName));

            });
            return collection;

        }
    }
}