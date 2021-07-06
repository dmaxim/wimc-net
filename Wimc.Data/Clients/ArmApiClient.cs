using System.Collections.Generic;
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

        private readonly IList<string> _apiVersions  = new List<string>()
        {
            "2021-05-01",
            "2021-03-01",
            "2021-02-01",
            "2020-11-01",
            "2020-09-01",
            "2020-08-01",
            "2016-07-01",
            "2015-06-15",
        };
        public ArmApiClient(HttpClient httpClient, IOptions<ArmApiClientConfig> configuration)
        {
            HttpClient = httpClient;
            _apiConfiguration = configuration.Value;
        }


        public HttpClient HttpClient { get; }

        public async Task<string> GetResourceDefinition(string resourceId)
        {
            return await GetResource(resourceId).ConfigureAwait(false);
        }

        public async Task<string> GetResourceContainerDefinition(string resourceContainerName)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"subscriptions/{_apiConfiguration.SubscriptionId}/resourcegroups/{resourceContainerName}/resources?{_apiConfiguration.ApiVersion}");

            using var response = await HttpClient.SendAsync(request).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new MxNotFoundException($"Resource container {resourceContainerName} was not found");
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new MxApplicationStateException(responseContent);
            }

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);


        }

        private async Task<string> GetResource(string resourceId)
        {
            var lastResponse = string.Empty;
            foreach (var apiVersion in _apiVersions)
            {
                using var request =
                    new HttpRequestMessage(HttpMethod.Get, $"{resourceId}?api-version={apiVersion}");
            
                using var response = await HttpClient.SendAsync(request).ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new MxNotFoundException($"Resource {resourceId} was not found");
                }

                lastResponse =await response.Content.ReadAsStringAsync().ConfigureAwait(false); 
                if (response.StatusCode != HttpStatusCode.BadRequest)
                {
                    return lastResponse;
                }
                
            }

            return lastResponse;

        }
        
    }
}