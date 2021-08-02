using System.Collections.Generic;
using System.Linq;
using Wimc.Domain.Models;

namespace Wimc.Models.Audit
{
    public class AuditIndexViewModel
    {
        public AuditIndexViewModel(IList<Resource> newResources)
        {
            NewResources = newResources.Select(resource => new AuditNewResourceViewModel(resource)).ToList();
        }
        
        public IList<AuditNewResourceViewModel> NewResources { get; }
    }
}