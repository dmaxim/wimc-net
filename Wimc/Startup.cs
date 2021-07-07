using System;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Wimc.Infrastructure;
using Wimc.Infrastructure.DI;
using Wimc.Infrastructure.Logging.Middleware;

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
           /*
              ConfigureAuthentication(services);
            
              services.AddAuthorization(config =>
              {
                  var authenticationPolicy = new AuthorizationPolicyBuilder()
                      .RequireAuthenticatedUser()
                      .Build();
  
                  config.DefaultPolicy = authenticationPolicy;
              });
              */
            services.AddControllersWithViews();
            services.AddAppDependencies(Configuration);
            //AddDataProtection(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogWarning("In startup");
            app.UseCustomExceptionHandler("Wimc-net", "/home/error");
            
            app.UseStaticFiles();

            app.UseRouting();

            /*
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy(new CookiePolicyOptions{ 
                MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax, 
                HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.None,
                Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.None
            });
            */
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(authOptions =>
                {
                    authOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                    
                    
                })
                .AddAzureAd(options => Configuration.Bind("AzureAd", options))
                .AddCookie(options => {
                
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
