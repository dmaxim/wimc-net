using System;
using System.Collections.Generic;
using System.Linq;

namespace Wimc.Domain.Models
{
    public class ResourceComparison
    {
        public ResourceComparison(IList<Resource> existing, IList<Resource> remote, int containerId, string containerName)
        {
            Deleted = GetDeleted(existing, remote);
            New = GetNew(existing, remote, containerId);
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
                existingResource.ResourceType.Equals(remoteResource.ResourceType, StringComparison.InvariantCultureIgnoreCase) &&
                existingResource.CloudId.Equals(remoteResource.CloudId, StringComparison.InvariantCultureIgnoreCase)))
                .ToList();
            
        }

        private IList<Resource> GetNew(IList<Resource> existing, IList<Resource> remote, int containerId)
        {
             return remote
                 .Where(remoteResource => !existing.Any(existingResource =>
                     remoteResource.ResourceType.Equals( existingResource.ResourceType, StringComparison.InvariantCultureIgnoreCase) &&
                     remoteResource.CloudId.Equals(existingResource.CloudId, StringComparison.InvariantCultureIgnoreCase)))
                 .Select(resource => new Resource
                 {
                     ResourceDefinition = resource.ResourceDefinition,
                     ResourceId = resource.ResourceId,
                     ResourceLocation = resource.ResourceLocation,
                     ResourceName = resource.ResourceName,
                     CloudId = resource.CloudId,
                     ResourceType = resource.ResourceType,
                     ResourceContainerId = containerId
                 })
                 .ToList();

        }
        
        
        
    }
}