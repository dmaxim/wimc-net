using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

namespace Wimc.Infrastructure.DI
{
    public static class Dependencies
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<WimcUIConfiguration>(config.GetSection("WimcConfig"))
                .PostConfigure<WimcUIConfiguration>(options => options.ThrowIfInvalid());

            services.Configure<ArmApiClientConfig>(config.GetSection("ArmApiClientConfig"));

            services.Configure<MessageBusConfiguration>(config.GetSection("MessageBus"));

            services.Configure<AzureTableConfiguration>(config.GetSection("AzureTable"));
            
            services.AddScoped<IEntityContext>(provider =>
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

            services.AddHttpClient<IApiClient, ArmApiClient>()
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
                
                
            services.AddRebus((configurer, provider) =>
            {
                var busConfig = provider.GetRequiredService<IOptions<MessageBusConfiguration>>().Value;
                return configurer
                    .Transport(t => t.UseAzureServiceBusAsOneWayClient(busConfig.ConnectionString));

            });
            services.AddTransient<IMessageClient, MessageClient>(provider =>
            {
                var configuration = provider.GetRequiredService<IOptions<MessageBusConfiguration>>().Value;
                var bus = provider.GetRequiredService<IBus>();
                return new MessageClient(configuration.ConnectionString, bus);
            });

            services.AddTransient<IAuditRepository, AuditRepository>(provider =>
            {
                var configuration = provider.GetRequiredService<IOptions<AzureTableConfiguration>>().Value;
                return new AuditRepository(configuration);
            });
                        
            services.AddTransient<IResourceContainerRepository, ResourceContainerRepository>();
            services.AddTransient<IResourceRepository, ResourceRepository>();
            services.AddTransient<IResourceContainerManager, ResourceContainerManager>();
            services.AddTransient<IResourceManager, ResourceManager>();
            services.AddTransient<IResourceQueryManager, ResourceQueryManager>();
            services.AddTransient<IAuditResultManager, AuditResultManager>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IBillingRepository, BillingRepository>();
            services.AddTransient<ICostManagementManager, CostManagementManager>();

            return services;
        }
    }
}