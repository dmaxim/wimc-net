using Wimc.Domain.Models;

namespace Wimc.Models
{
    public class NewAzureResourceViewModel : AzureResourceViewModel
    {
        public NewAzureResourceViewModel() {}
        public NewAzureResourceViewModel(Resource resource) :base(resource){}
        
        public  NewAzureResourceViewModel(Resource resource, int resourceContainerId) : base(resource, resourceContainerId) {} 
    }
}