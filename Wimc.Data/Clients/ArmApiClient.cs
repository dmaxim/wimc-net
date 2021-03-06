using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Mx.Library.ExceptionHandling;
using Mx.Library.Serialization;
using Wimc.Domain.AppConfiguration;
using Wimc.Domain.Clients;
using Wimc.Domain.Models.CostManagement;

namespace Wimc.Data.Clients
{
    public class ArmApiClient : IApiClient
    {
        private readonly ArmApiClientConfig _apiConfiguration;
        private const string BillingQueryTemplate = "subscriptions/{0}/resourcegroups/{1}/providers/Microsoft.CostManagement/query?api-version=2019-11-01";
  
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
            using var request = new HttpRequestMessage(HttpMethod.Get,
                $"subscriptions/{_apiConfiguration.SubscriptionId}/resourcegroups/{resourceContainerName}/resources?{_apiConfiguration.ApiVersion}");

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

        public async Task<string> ExecuteQuery(string uri, string apiVersion)
        {
            using var request =
                new HttpRequestMessage(HttpMethod.Get, $"{uri}?api-version={apiVersion}");
            
            using var response = await HttpClient.SendAsync(request).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return "Not Found";
            }
            else
            {
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        public async Task<QueryResult> GetResourceContainerBilling(BillingQuery query)
        {
            var queryUrl = string.Format(BillingQueryTemplate, _apiConfiguration.SubscriptionId, query.ContainerName);
            var content = new StringContent(query.Query.ToJson(), Encoding.UTF8, "application/json");
            using var response = await HttpClient.PostAsync(
                new Uri($"{HttpClient.BaseAddress}/{queryUrl}"),
                content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return responseContent.DeserializeJson<QueryResult>();
            }
            else
            {
                return null;
            }
        }

        private async Task<string> GetResource(string resourceId)
        {
            var lastResponse = string.Empty;
            foreach (var apiVersion in _apiConfiguration.ApiVersions)
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