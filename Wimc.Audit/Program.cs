﻿using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Wimc.Business.Managers;


namespace Wimc.Audit
{
    class Program
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
        static void Main(string[] args)
        {
            var auditManager = ServiceProvider.GetRequiredService<IAuditManager>();
            auditManager.Generate().ConfigureAwait(false).GetAwaiter().GetResult();
        }

    }
}