using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mx.EntityFramework.Contracts;
using Mx.EntityFramework.Repositories;
using Wimc.Domain.Clients;
using Wimc.Domain.Models;
using Wimc.Domain.Repositories;

namespace Wimc.Data.Repositories
{
    public class ResourceRepository : Repository<Resource>, IResourceRepository
    {
        private readonly IApiClient _apiClient;
        public ResourceRepository(IEntityContext entityContext, IApiClient apiClient) : base(entityContext)
        {
            _apiClient = apiClient;
        }

        public async Task<IList<string>> GetDistinctResources()
        {
            return await GetAll()
                .Select(resource => resource.ResourceType)
                .Distinct().ToListAsync().ConfigureAwait(false);
        }

        public async Task<string> GetResourceDefinition(string resourceId)
        {
            return await _apiClient.GetResourceDefinition(resourceId).ConfigureAwait(false);
        }
    }
}