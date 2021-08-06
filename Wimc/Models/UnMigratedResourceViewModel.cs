using Wimc.Domain.Models;

namespace Wimc.Models
{
    public class UnMigratedResourceViewModel : AzureResourceViewModel
    {
        public UnMigratedResourceViewModel() {}
        public UnMigratedResourceViewModel(Resource resource) : base(resource) { }

        public UnMigratedResourceViewModel(Resource resource, int resourceContainerId) : base(resource)
        {
            ResourceContainerId = resourceContainerId;
        }
    }
}