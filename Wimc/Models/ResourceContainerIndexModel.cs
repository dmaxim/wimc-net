using System.Collections.Generic;
using System.Linq;
using Wimc.Domain.Models;

namespace Wimc.Models
{
    public class ResourceContainerIndexModel
    {
        public ResourceContainerIndexModel(IList<ResourceContainer> resources)
        {
            if (resources == null)
            {
                Resources = new List<ResourceContainerViewModel>();
            }
            else
            {
                Resources = resources.Select(resource => new ResourceContainerViewModel(resource)).ToList();
            }
            
        }
        
        public IList<ResourceContainerViewModel> Resources { get; }
    }
}