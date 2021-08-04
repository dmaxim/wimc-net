using Wimc.Domain.Models;

namespace Wimc.Domain.Messages.Events
{
    public class ResourceAdded : WimcEvent
    {
        public ResourceAdded() {}
        public ResourceAdded(Resource newResource) : base(newResource)
        {
            
        }


        public override string Topic => "resourceadded";
        public override string Subject => "NewResource";


    }
}