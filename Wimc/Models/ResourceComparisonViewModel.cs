using System.Collections.Generic;
using System.Linq;
using Wimc.Domain.Models;

namespace Wimc.Models
{
    public class ResourceComparisonViewModel
    {
        public ResourceComparisonViewModel(ResourceComparison resourceComparison)
        {
            ContainerId = resourceComparison.ResourceContainerId;
            ContainerName = resourceComparison.ContainerName;
            Deleted = resourceComparison.Deleted.Select(resource => new AzureResourceViewModel(resource, resourceComparison.ResourceContainerId))
                .OrderBy(resource => resource.Name)
                .ToList();
            New = resourceComparison.New.Select(resource => new NewAzureResourceViewModel(resource, resourceComparison.ResourceContainerId)).ToList();
        }
        public int ContainerId { get; }
        public string ContainerName { get; }
        
        public IList<AzureResourceViewModel> Deleted { get; }
        
        public IList<NewAzureResourceViewModel> New { get; }
    }
}