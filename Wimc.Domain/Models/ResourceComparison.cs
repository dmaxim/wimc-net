using System.Collections.Generic;
using System.Linq;

namespace Wimc.Domain.Models
{
    public class ResourceComparison
    {
        public ResourceComparison(IList<Resource> existing, IList<Resource> remote, int containerId, string containerName)
        {
            Deleted = GetDeleted(existing, remote);
            New = GetNew(existing, remote);
            ResourceContainerId = containerId;
            ContainerName = containerName;
        }
        
        public IList<Resource> Deleted { get; }
        
        public IList<Resource> New { get; }

        public int ResourceContainerId { get; }
        
        public string ContainerName { get;  }
        private IList<Resource> GetDeleted(IList<Resource> existing, IList<Resource> remote)
        {
            return existing
                .Where(existingResource => !remote.Any(remoteResource =>
                existingResource.ResourceType == remoteResource.ResourceType &&
                existingResource.ResourceName == remoteResource.ResourceName))
                .ToList();
            
        }

        private IList<Resource> GetNew(IList<Resource> existing, IList<Resource> remote)
        {
            return remote
                .Where(remoteResource => !existing.Any(existingResource =>
                    remoteResource.ResourceType == existingResource.ResourceType &&
                    remoteResource.ResourceName == existingResource.ResourceName))
                .ToList();

        }
        
        
        
    }
}