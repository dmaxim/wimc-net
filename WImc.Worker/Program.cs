using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;

namespace WImc.Worker
{
    public class Program
    {
        //private static readonly IServiceProvider ServiceProvider;
        static Program()
        {
            // var applicationEnvironment = PlatformServices.Default.Application;
            //
            // //setup configuration
            // var environment = Environment.GetEnvironmentVariable("RuntimeEnvironment") ?? "";
            //
            // var configuration = new ConfigurationBuilder()
            //     .SetBasePath(applicationEnvironment.ApplicationBasePath)
            //     .AddJsonFile("appsettings.json")
            //     .AddJsonFile("appsettings.secrets.json", optional: true)
            //     .AddEnvironmentVariables().Build();
            //
            // var collection = new ServiceCollection();
            // collection.AddOptions().AddWorkerDependencies(configuration);
            //
            //
            // ServiceProvider = collection.BuildServiceProvider();
            
        }
        
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.AddJsonFile("appsettings.json", false, reloadOnChange: true);
                    configurationBuilder.AddJsonFile("appsettings.secrets.json", true, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions().AddWorkerDependencies(hostContext.Configuration);
                    services.AddHostedService<Worker>();
                    
                });
    }
}