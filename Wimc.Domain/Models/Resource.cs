using Mx.Library.Serialization;

namespace Wimc.Domain.Models
{
    public class Resource
    {
        public Resource() {}

        public Resource(AzureResource azureResource)
        {
            ResourceName = azureResource.Name;
            ResourceType = azureResource.Type;
            CloudId = azureResource.Id;
            ResourceDefinition = azureResource.ToJson();
            ResourceLocation = !string.IsNullOrWhiteSpace(azureResource.Location) ? azureResource.Location : "NA";
            IsMigrated = false;
            Notes = azureResource.Notes;
        }

        public Resource(AzureResource azureResource, string resourceJson) : this(azureResource)
        {
            ResourceDefinition = resourceJson;
        }
        public int ResourceId { get; set; }
        
        public int ResourceContainerId { get; set; }
        
        public string ResourceName { get; set; }
        
        public string ResourceType { get; set; }
        
        public string CloudId { get; set; }
        
        public string ResourceLocation { get; set; }
        public string ResourceDefinition { get; set; }
         
        public bool IsMigrated { get; set; }
        
        public string Notes { get; set; }
    }
}