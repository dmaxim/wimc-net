using System.Collections.Generic;
using System.Linq;

namespace Wimc.Models
{
    public class ResourceTypesViewModel
    {
        public ResourceTypesViewModel(IList<string> resourceTypes)
        {
            ResourceTypes = resourceTypes.Select(resourceType => new ResourceTypeViewModel(resourceType)).ToList();
        }
        
        public IList<ResourceTypeViewModel> ResourceTypes { get;  }
    }
}