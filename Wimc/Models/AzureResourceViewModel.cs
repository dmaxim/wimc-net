
using System.Collections.Generic;

namespace Wimc.Models
{
    public class AzureResourceViewModel
    {
        public string Id { get; set; }
        public string Location { get; set; }

        public string Name { get; set; }
        public string ResourceGroup { get; set; }

        public ResourceSkuViewModel Sku { get; set; }

        public string Type { get; set; }
    }
}
