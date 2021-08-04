using System.Collections.Generic;
using System.Linq;
using Wimc.Domain.Models;

namespace Wimc.Models.Audit
{
    public class AuditIndexViewModel
    {
        public AuditIndexViewModel(IList<ResourceContainer> resourceContainers)
        {
            Containers = resourceContainers.Select(container => new AuditContainerViewModel(container)).ToList();
        }
        
        public IList<AuditContainerViewModel> Containers { get; }
    }
}