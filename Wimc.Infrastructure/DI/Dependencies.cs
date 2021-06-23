using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mx.EntityFramework.Contracts;
using Mx.Library.ExceptionHandling;
using Wimc.Business.Managers;
using Wimc.Data.Contexts;
using Wimc.Data.Repositories;
using Wimc.Domain.Repositories;
using Wimc.Infrastructure.Configuration;

namespace Wimc.Infrastructure.DI
{
    public static class Dependencies
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<WimcUIConfiguration>(config.GetSection("WimcConfig"))
                .PostConfigure<WimcUIConfiguration>(options => options.ThrowIfInvalid());

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
            services.AddTransient<IResourceContainerRepository, ResourceContainerRepository>();
            services.AddTransient<IResourceRepository, ResourceRepository>();
            services.AddTransient<IResourceContainerManager, ResourceContainerManager>();

            return services;
        }
    }
}