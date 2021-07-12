
using System.Collections.Generic;
using Wimc.Domain.Models;

namespace Wimc.Models
{
    public class AzureResourceViewModel
    {
        public AzureResourceViewModel() {}
        public AzureResourceViewModel(Resource resource)
        {
            ResourceId = resource.ResourceId;
            Id = resource.CloudId;
            Name = resource.ResourceName;
            Type = resource.ResourceType;
            Location = resource.ResourceLocation;
            IsMigrated = resource.IsMigrated;
            Definition = resource.ResourceDefinition;
            Notes = resource.Notes;
        }

        public AzureResourceViewModel(Resource resource, int resourceContainerId) : this(resource)
        {
            ResourceContainerId = resourceContainerId;
        }
        
        public int ResourceContainerId { get; set; }
        public int ResourceId { get; set;  }
        public string Id { get; set; }
        public string Location { get; set; }

        public string Name { get; set; }
        public string ResourceGroup { get; set; }

        public bool IsMigrated { get; set; }
        
        public ResourceSkuViewModel Sku { get; set; }

        public string Type { get; set; }
        
        public string Definition { get; set; }
        
        public string Notes { get; set; }
    }
}
