using System.Collections.Generic;
using System.Linq;
using Wimc.Domain.Models;

namespace Wimc.Models.Audit
{
    public class AuditContainerViewModel
    {
        public AuditContainerViewModel(ResourceContainer resourceContainer)
        {
            ResourceContainerId = resourceContainer.ResourceContainerId;
            Name = resourceContainer.ContainerName;
            Resources = resourceContainer.Resources.Select(resource => new AuditNewResourceViewModel(resource))
                .ToList();
        }
        
        public int ResourceContainerId { get;  }
        
        public string Name { get;  }
        public IList<AuditNewResourceViewModel> Resources { get; }
    }
}