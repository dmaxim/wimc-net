using Wimc.Domain.Models;

namespace Wimc.Models.Audit
{
    public class AuditNewResourceViewModel
    {
        public AuditNewResourceViewModel(Resource resource)
        {
            ResourceContainerId = resource.ResourceContainerId;
            Name = resource.ResourceName;
            CloudId = resource.CloudId;
        }
        
        public int ResourceContainerId { get; }
        public string Name { get;  }
        public string CloudId { get;  }
        
    }
}