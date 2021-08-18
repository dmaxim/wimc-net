using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Wimc.Domain.Clients;

namespace Wimc.Handler
{
    static class Program
    {
        private static readonly IServiceProvider ServiceProvider;
        
        static Program()
        {
            var applicationEnvironment = PlatformServices.Default.Application;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(applicationEnvironment.ApplicationBasePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.secrets.json", optional: true)
                .AddEnvironmentVariables().Build();

            var collection = new ServiceCollection();
            collection.AddOptions().AddHandlerServiceDependencies(configuration);


            ServiceProvider = collection.BuildServiceProvider();
            
        }
        
        static async Task Main(string[] args)
        {
            var messageClient = ServiceProvider.GetRequiredService<IMessageClient>();
            await messageClient.Receive("addresource", "addresource").ConfigureAwait(false);
            
            
        }
    }
}