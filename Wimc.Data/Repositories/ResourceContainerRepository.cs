using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mx.EntityFramework.Contracts;
using Mx.EntityFramework.Repositories;
using Wimc.Domain.Models;
using Wimc.Domain.Repositories;

namespace Wimc.Data.Repositories
{
    public class ResourceContainerRepository : Repository<ResourceContainer>, IResourceContainerRepository
    {
        public ResourceContainerRepository(IEntityContext entityContext) : base(entityContext) {}
        public async Task<ResourceContainer> Get(int resourceContainerId)
        {
            return await GetAll()
                .Include(resourceContainer => resourceContainer.Resources)
                .FirstOrDefaultAsync(resourceContainer =>
                    resourceContainer.ResourceContainerId.Equals(resourceContainerId));


        }
    }
}