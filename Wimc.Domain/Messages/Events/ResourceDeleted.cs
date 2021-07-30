using Wimc.Domain.Models;

namespace Wimc.Domain.Messages.Events
{
    public class ResourceDeleted : WimcEvent
    {
        public ResourceDeleted() {}
        public ResourceDeleted(Resource deletedResource) : base(deletedResource)
        {
            
        }


        public override string Topic => "resourcedeleted";
        public override string Subject => "NewResource";

    }
}