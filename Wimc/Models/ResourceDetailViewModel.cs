using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wimc.Domain.Models;

namespace Wimc.Models
{
    public class ResourceDetailViewModel
    {
        public ResourceDetailViewModel(string name, string json, int containerId, ICollection<Resource> resources)
        {
            Name = name;
            Json = json;
            Resources = resources.Select(resource => new AzureResourceViewModel(resource, containerId))
                .OrderBy(resource => resource.Name)
                .ToList();
            UnMigratedResources = resources.Where(
                    resource => !resource.IsMigrated)
                .Select(resource => new UnMigratedResourceViewModel(resource, containerId))
                .OrderBy(resource => resource.Name)
                .ToList();
        }
        public string Name { get; set; }

        public string Json { get; set; }
        public IList<AzureResourceViewModel> Resources { get; set; }
        
        public IList<UnMigratedResourceViewModel> UnMigratedResources { get; }
        

    }
}
