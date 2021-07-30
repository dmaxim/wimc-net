using Wimc.Domain.Models;

namespace Wimc.Domain.Messages.Events
{
    public abstract class WimcEvent
    {
        public WimcEvent() {}
        public WimcEvent(Resource resource)
        {
            ResourceContainerId = resource.ResourceContainerId;
            CloudId = resource.CloudId;
            Name = resource.ResourceName;
        }
        
        public int ResourceContainerId { get; set; }
        public string CloudId { get; set; }
        public string Name { get; set; }

        public abstract string Topic
        {
            get;

        }
        
        public abstract string Subject { get; }


    }
}