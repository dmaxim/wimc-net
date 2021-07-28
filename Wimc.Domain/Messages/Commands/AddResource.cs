using Wimc.Domain.Models;

namespace Wimc.Domain.Messages.Commands
{
    public class AddResource
    {
        public AddResource(Resource newResource)
        {
            CloudId = newResource.CloudId;
            Name = newResource.ResourceName;
        }
        
        public string CloudId { get; set; }
        public string Name { get; set; }
       
    }
}