using Wimc.Domain.Models;

namespace Wimc.Domain.Messages.Events
{
    public class ResourceDeleted
    {
        public ResourceDeleted() {}
        public ResourceDeleted(Resource newResource)
        {
            CloudId = newResource.CloudId;
            Name = newResource.ResourceName;
            ResourceContainerId = newResource.ResourceContainerId;
        }
        
        public int ResourceContainerId { get; set; }
        public string CloudId { get; set; }
        public string Name { get; set; }
    }
}