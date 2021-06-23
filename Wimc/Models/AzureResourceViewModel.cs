
using System.Collections.Generic;
using Wimc.Domain.Models;

namespace Wimc.Models
{
    public class AzureResourceViewModel
    {
        public AzureResourceViewModel(Resource resource)
        {
            Id = resource.CloudId;
            Name = resource.ResourceName;
            Type = resource.ResourceType;
        }
        public string Id { get; set; }
        public string Location { get; set; }

        public string Name { get; set; }
        public string ResourceGroup { get; set; }

        public ResourceSkuViewModel Sku { get; set; }

        public string Type { get; set; }
    }
}
