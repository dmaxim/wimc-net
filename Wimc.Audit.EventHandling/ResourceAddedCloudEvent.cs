using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Wimc.Audit.EventHandling
{
    public class ResourceAddedCloudEvent
    {
        [JsonPropertyName("subject")] public string Subject { get; set; }

        public string topic { get; set; }

        public string eventType { get; set; }
        
        public string data { get; set; }

        public ResourceAdded ResourceAdded
        {
            get
            {
                return JsonConvert.DeserializeObject<ResourceAdded>(data);
            }
        }
    }

}