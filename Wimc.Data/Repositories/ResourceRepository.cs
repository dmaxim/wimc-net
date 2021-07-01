using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mx.EntityFramework.Contracts;
using Mx.EntityFramework.Repositories;
using Wimc.Domain.Models;
using Wimc.Domain.Repositories;

namespace Wimc.Data.Repositories
{
    public class ResourceRepository : Repository<Resource>, IResourceRepository
    {
        public ResourceRepository(IEntityContext entityContext) : base(entityContext)
        {
        }

        public async Task<IList<string>> GetDistinctResources()
        {
            return await GetAll()
                .Select(resource => resource.ResourceType)
                .Distinct().ToListAsync().ConfigureAwait(false);
        }
    }
}