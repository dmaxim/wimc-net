using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wimc.Domain.Models;

namespace Wimc.Models
{
    public class ResourceDetailViewModel
    {
        public ResourceDetailViewModel(string name, ICollection<Resource> resources)
        {
            Name = name;
            Resources = resources.Select(resource => new AzureResourceViewModel(resource)).ToList();
        }
        public string Name { get; set; }

        public IList<AzureResourceViewModel> Resources { get; set; }
    }
}
