using Wimc.Domain.Models;

namespace Wimc.Models
{
    public class EditResourceContainerViewModel
    {
        public EditResourceContainerViewModel()
        {
            
        }

        public EditResourceContainerViewModel(ResourceContainer resourceContainer)
        {
            ResourceContainerId = resourceContainer.ResourceContainerId;
            Name = resourceContainer.ContainerName;
        }
        
        public int ResourceContainerId { get; set; }
        
        public string Name { get; set; }
    }
}