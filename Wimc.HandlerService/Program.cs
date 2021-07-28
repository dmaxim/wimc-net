using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Wimc.Domain.Clients;
using Wimc.Handler;

namespace Wimc.HandlerService
{
    class Program
    {
        private static readonly IServiceProvider ServiceProvider;
        
        static Program()
        {
            var applicationEnvironment = PlatformServices.Default.Application;

            //setup configuration
            var environment = Environment.GetEnvironmentVariable("RuntimeEnvironment") ?? "";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(applicationEnvironment.ApplicationBasePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.secrets.json", optional: true)
                .AddEnvironmentVariables().Build();

            var collection = new ServiceCollection();
            collection.AddOptions().AddHandlerServiceDependencies(configuration);


            ServiceProvider = collection.BuildServiceProvider();
            
        }
        
        static void Main(string[] args)
        {
            //var messageClient = ServiceProvider.GetService<IMessageClient>();
          
            //await messageClient.Receive("addresource", "addresource").ConfigureAwait(false);
            var rebus = new RebusBootstrapper(ServiceProvider);

            rebus.Start();
            
            
            Thread.Sleep(10000);

            rebus.Stop();



        }
    }
}