
using System.Collections.Generic;

namespace Wimc.Models
{
    public class AzureResource
    {
        public string Id { get; set; }
        public string Location { get; set; }

        public string Name { get; set; }
        public string ResourceGroup { get; set; }

        public ResourceSku Sku { get; set; }

        public string Type { get; set; }
    }
}
