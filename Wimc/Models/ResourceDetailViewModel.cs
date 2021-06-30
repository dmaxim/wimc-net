using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wimc.Domain.Models;

namespace Wimc.Models
{
    public class ResourceDetailViewModel
    {
        public ResourceDetailViewModel(string name, string json, ICollection<Resource> resources)
        {
            Name = name;
            Json = json;
            Resources = resources.Select(resource => new AzureResourceViewModel(resource)).ToList();
        }
        public string Name { get; set; }

        public string Json { get; set; }
        public IList<AzureResourceViewModel> Resources { get; set; }
    }
}
