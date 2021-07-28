using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Wimc.Data.Clients;
using Wimc.Domain.Clients;

namespace Wimc.Handler
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
        
        static async Task Main(string[] args)
        {
            var messageClient = ServiceProvider.GetService<IMessageClient>();
            
           // var messageClient =
             //   new MessageClient(
               //     "Endpoint=sb://asb-mxinfo-messaging-dev.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=UlyaxTj0smGKxznkWaON8OmKLSn72HfHGrSePHRo70I=");

            await messageClient.Receive("addresource", "addresource").ConfigureAwait(false);
            
            
        }
    }
}