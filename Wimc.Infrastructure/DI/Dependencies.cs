using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wimc.Business.Managers;
using Wimc.Data.Repositories;
using Wimc.Domain.Repositories;
using Wimc.Infrastructure.Configuration;

namespace Wimc.Infrastructure.DI
{
    public static class Dependencies
    {
        public static IServiceCollection AddDemoAppDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<WimcUIConfiguration>(config.GetSection("DemoAppConfig"))
                .PostConfigure<WimcUIConfiguration>(options => options.ThrowIfInvalid());

            
            services.AddTransient<IResourceContainerRepository, ResourceContainerRepository>();
            services.AddTransient<IResourceRepository, ResourceRepository>();
            services.AddTransient<IResourceContainerManager, ResourceContainerManager>();

            return services;
        }
    }
}