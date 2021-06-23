using Wimc.Domain.Models;

namespace Wimc.Models
{
    public class ResourceContainerViewModel
    {
        public ResourceContainerViewModel(ResourceContainer resourceContainer)
        {
            ResourceContainerId = resourceContainer.ResourceContainerId;
            ContainerName = resourceContainer.ContainerName;
        }
        public int ResourceContainerId { get;  }
        
        public string ContainerName { get;  }
    }
}