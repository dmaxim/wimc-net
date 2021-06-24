using System;
using Azure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wimc.Infrastructure.DI;

namespace Wimc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAppDependencies(Configuration);
            AddDataProtection(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void AddDataProtection(IServiceCollection services)
        {
            var azureStorageConnectionString = Configuration["AzureStorage:ConnectionString"];
            var keyVaultUri = Configuration["DataProtection:KeyIdentifier"];
            services.AddDataProtection()
                .PersistKeysToAzureBlobStorage(azureStorageConnectionString, "dapi", "wimcnet")
                .ProtectKeysWithAzureKeyVault(new Uri(keyVaultUri), new DefaultAzureCredential());

        }
        
    }
}
