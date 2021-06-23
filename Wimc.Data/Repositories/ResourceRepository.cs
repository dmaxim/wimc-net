using Mx.EntityFramework.Contracts;
using Mx.EntityFramework.Repositories;
using Wimc.Domain.Models;
using Wimc.Domain.Repositories;

namespace Wimc.Data.Repositories
{
    public class ResourceRepository : Repository<Resource>, IResourceRepository
    {
        public ResourceRepository(IEntityContext entityContext) : base(entityContext){}
    }
}