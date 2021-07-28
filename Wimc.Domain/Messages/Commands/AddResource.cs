using Wimc.Domain.Models;

namespace Wimc.Domain.Messages.Commands
{
    public class AddResource
    {
        public AddResource(Resource newResource)
        {
            NewResource = newResource;
        }
        public Resource NewResource {get; set; }   
    }
}