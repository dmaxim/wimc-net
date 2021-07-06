using System.Collections.Generic;

namespace Wimc.Models
{
    public class ContainerTemplateViewModel
    {
        public ContainerTemplateViewModel(string containerName, IList<ResourceTemplateViewModel> templates)
        {
            ContainerName = containerName;
            Templates = templates;
        }
        
        public string ContainerName { get;  }
        
        public IList<ResourceTemplateViewModel> Templates { get;  }
    }
}