using System.Collections.Generic;
using System.Linq;
using Wimc.Domain.Models;

namespace Wimc.Models
{
    public class ResourceListViewModel
    {
        public ResourceListViewModel(string resourceType, IList<Resource> resources)
        {
            ResourceType = resourceType;
            Resources = resources.Select(resource => new ResourceTypeResourceViewModel(resource)).ToList();
       
        }
        
        public string ResourceType { get; }
        
        public IList<ResourceTypeResourceViewModel> Resources { get; }
        
     
    }
}