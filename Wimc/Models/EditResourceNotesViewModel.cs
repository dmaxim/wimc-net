using Wimc.Domain.Models;

namespace Wimc.Models
{
    public class EditResourceNotesViewModel
    {
        public EditResourceNotesViewModel() {}

        public EditResourceNotesViewModel(Resource resource, int containerId)
        {
            ResourceContainerId = containerId;
            ResourceId = resource.ResourceId;
            Notes = resource.Notes;
        }
        
        public int ResourceContainerId { get; set; }
        public int ResourceId { get; set; }
        public string Notes { get; set; }
        
    }
}