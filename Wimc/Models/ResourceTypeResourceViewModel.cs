using Wimc.Domain.Models;

namespace Wimc.Models
{
    public class ResourceTypeResourceViewModel : AzureResourceViewModel
    {
           public ResourceTypeResourceViewModel(Resource resource) : base(resource) {}
           
           public ResourceTypeResourceViewModel() {}
    }
}