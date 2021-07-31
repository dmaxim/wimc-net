using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

             ProcessResourceAddedEvent(resourceAddedCloudEvent);

        }


        private static void ProcessResourceAddedEvent(ResourceAddedCloudEvent cloudEvent)
        {
            
            // Write resource added to data store
        }
        
    }

}