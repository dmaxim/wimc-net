using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Mx.Library.ExceptionHandling;
using Wimc.Domain.AppConfiguration;
using Wimc.Domain.Clients;

namespace Wimc.Data.Clients
{
    public class ArmApiClient : IApiClient
    {
        private readonly ArmApiClientConfig _apiConfiguration; 

        public ArmApiClient(HttpClient httpClient, IOptions<ArmApiClientConfig> configuration)
        {
            HttpClient = httpClient;
            _apiConfiguration = configuration.Value;
        }


        public HttpClient HttpClient { get; }

        public async Task<string> GetResourceDefinition(string resourceId)
        {
            using var request =
                new HttpRequestMessage(HttpMethod.Get, $"{resourceId}?{_apiConfiguration.ApiVersion}");
            
            using var response = await HttpClient.SendAsync(request).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new MxNotFoundException($"Resource {resourceId} was not found");
            }

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}