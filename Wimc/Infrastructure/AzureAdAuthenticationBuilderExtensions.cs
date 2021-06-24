using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Wimc.Infrastructure
{
 public static class AzureAdAuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddAzureAd(this AuthenticationBuilder builder)
            => builder.AddAzureAd(_ => { });

        public static AuthenticationBuilder AddAzureAd(this AuthenticationBuilder builder, Action<AzureAdOptions> configureOptions)
        {
            builder.Services.Configure(configureOptions);
            builder.Services.AddSingleton<IConfigureOptions<OpenIdConnectOptions>, ConfigureAzureOptions>();
            builder.AddOpenIdConnect();
            return builder;
        }

        private class ConfigureAzureOptions : IConfigureNamedOptions<OpenIdConnectOptions>
        {
            private readonly AzureAdOptions _azureOptions;

            public ConfigureAzureOptions(IOptions<AzureAdOptions> azureOptions)
            {
                _azureOptions = azureOptions.Value;
            }

            public void Configure(string name, OpenIdConnectOptions options)
            {
                options.ClientId = _azureOptions.ClientId;
                options.Authority = $"{_azureOptions.Instance}{_azureOptions.TenantId}";
                options.UseTokenLifetime = true;
                options.CallbackPath = _azureOptions.CallbackPath;
                options.RequireHttpsMetadata = true; // Changed this to true does it break anything
				options.ClaimActions.DeleteClaim("sid");
				options.ClaimActions.DeleteClaim("idp");

	            options.Events = new OpenIdConnectEvents()
	            {
		            OnRedirectToIdentityProvider = (context) =>
		            {
			            
			            if (context.Properties.Items[".redirect"] != null && context.Properties.Items[".redirect"].StartsWith("http://"))
			            {
				            context.Properties.Items[".redirect"] = context.Properties.Items[".redirect"].Replace("http:", "https:");
			            }

			            if (context.ProtocolMessage.RedirectUri.StartsWith("http://"))
			            {
				            context.ProtocolMessage.RedirectUri =
					            context.ProtocolMessage.RedirectUri.Replace("http:", "https:");
			            }
			            return Task.CompletedTask;
		            }
				};
            }

            public void Configure(OpenIdConnectOptions options)
            {
                Configure(Options.DefaultName, options);
            }
        }
    }

}