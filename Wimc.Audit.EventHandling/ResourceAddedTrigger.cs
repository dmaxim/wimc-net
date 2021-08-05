using System;
using System.Linq;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TableEntity = Azure.Data.Tables.TableEntity;


namespace Wimc.Audit.EventHandling
{
    public static class ResourceAddedTrigger
    {
        [FunctionName("ResourceAddedTrigger")]
        public static async Task RunAsync([ServiceBusTrigger("resourceadded", Connection = "ServiceBusConnection")]
            string resourceAddedEvent, ILogger log)
        {
                
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {resourceAddedEvent}");

             var resourceAddedCloudEvent = JsonConvert.DeserializeObject<ResourceAddedCloudEvent>(resourceAddedEvent);

             await ProcessResourceAddedEvent(resourceAddedCloudEvent).ConfigureAwait(false);

        }


        private static async Task ProcessResourceAddedEvent(ResourceAddedCloudEvent cloudEvent)
        {
            var accountName = System.Environment.GetEnvironmentVariable("AccountName");
            var accountKey = System.Environment.GetEnvironmentVariable("AccountKey");
            var endpointUrl = System.Environment.GetEnvironmentVariable("EndpointUrl");
            var tableName = System.Environment.GetEnvironmentVariable("TableName");

            if (string.IsNullOrWhiteSpace(endpointUrl))
            {
                throw new ArgumentNullException(nameof(endpointUrl));
            }
            if (cloudEvent?.ResourceAdded == null)
            {
                return;
            }

            var tableClient = new TableClient(
                new Uri(endpointUrl),
                tableName,
                new TableSharedKeyCredential(accountName,
                    accountKey)
            );

            if (!EntityExists(cloudEvent, tableClient))
            {
                await AddEvent(cloudEvent, tableClient).ConfigureAwait(false);

            }
        }

        private static bool EntityExists(ResourceAddedCloudEvent cloudEvent, TableClient tableClient)
        {
            var result =
                tableClient.Query<TableEntity>(
                    $"PartitionKey eq '{cloudEvent.ResourceAdded.ResourceContainerId}' and RowKey eq '{cloudEvent.ResourceAdded.RowKey}'");

            return result.Any();
        }

        private static async Task AddEvent(ResourceAddedCloudEvent cloudEvent, TableClient tableClient)
        {
            var resourceAdded = new TableEntity(cloudEvent.ResourceAdded.ResourceContainerId.ToString(), cloudEvent.ResourceAdded.RowKey )
            {
                { "ResourceAdded", cloudEvent.data}
            };

               await  tableClient.AddEntityAsync(resourceAdded).ConfigureAwait(false);


        }
        
    }

}