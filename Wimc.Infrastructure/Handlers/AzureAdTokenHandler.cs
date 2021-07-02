using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Wimc.Domain.AppConfiguration;

namespace Wimc.Infrastructure.Handlers
{

    public class AzureAdTokenHandler : DelegatingHandler
    {
        private readonly string _authority;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _apiResourceId;
        private readonly SemaphoreSlim _locker = new SemaphoreSlim(1);

        public AzureAdTokenHandler(ArmApiClientConfig config)
        {
            if (string.IsNullOrWhiteSpace(config.Resource))
                throw new ArgumentNullException(nameof(config.Resource));

            _apiResourceId = config.Resource;
            _authority = config.Instance;
            _clientId = config.ClientId;
            _clientSecret = config.ClientSecret;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            await AttachAuthorizationHeader(request).ConfigureAwait(false);
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        private async Task AttachAuthorizationHeader(HttpRequestMessage request)
        {
            try
            {
                await _locker.WaitAsync().ConfigureAwait(false);

                var context = new AuthenticationContext(_authority);
                var response = await context
                    .AcquireTokenAsync(_apiResourceId, new ClientCredential(_clientId, _clientSecret))
                    .ConfigureAwait(false);

                request.Headers.Authorization =
                    new AuthenticationHeaderValue(response.AccessTokenType, response.AccessToken);
            }
            finally
            {
                _locker.Release();
            }
        }
    }
}
