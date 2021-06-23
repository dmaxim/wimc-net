using Mx.EntityFramework.Contracts;
using Mx.EntityFramework.Repositories;
using Wimc.Domain.Models;
using Wimc.Domain.Repositories;

namespace Wimc.Data.Repositories
{
    public class ResourceContainerRepository : Repository<ResourceContainer>, IResourceContainerRepository
    {
        public ResourceContainerRepository(IEntityContext entityContext) : base(entityContext) {}
    }
}